using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.InversionOfControl;
using HansKindberg.VisualStudio.Templating.Wizards;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMapServiceLocator = HansKindberg.StructureMap.ServiceLocator;

namespace HansKindberg.VisualStudio.Templating
{
	public class Bootstrapper : IBootstrapper
	{
		#region Fields

		private static volatile bool _initialized;
		private static readonly object _initializedLock = new object();
		private static volatile IBootstrapper _instance;
		private static readonly object _instanceLock = new object();

		#endregion

		#region Properties

		public virtual bool Initialized
		{
			get { return _initialized; }
			protected internal set { _initialized = value; }
		}

		protected internal virtual object InitializedLock => _initializedLock;

		public static IBootstrapper Instance
		{
			get
			{
				// ReSharper disable InvertIf
				if(_instance == null)
				{
					lock(_instanceLock)
					{
						if(_instance == null)
							_instance = new Bootstrapper();
					}
				}
				// ReSharper restore InvertIf

				return _instance;
			}
			set
			{
				if(value == _instance)
					return;

				lock(_instanceLock)
				{
					_instance = value;
				}
			}
		}

		#endregion

		#region Methods

		protected internal virtual void ConfigureStructureMap(IProfileRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException(nameof(registry));

			registry.For<IServiceLocator>().Singleton().Use(ServiceLocator.Instance);
			registry.For<IWizardControllerFactory>().Singleton().Use<WizardControllerFactory>();
			registry.For<IWizardHost>().Singleton().Use<WizardHost>();
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "How should we handle it? When should we dispose it?")]
		public virtual void Initialize()
		{
			lock(this.InitializedLock)
			{
				if(this.Initialized)
					throw new InvalidOperationException("Already initialized.");

				var container = new Container();

				ServiceLocator.Instance = new StructureMapServiceLocator(container);

				container.Configure(this.ConfigureStructureMap);

				this.Initialized = true;
			}
		}

		#endregion
	}
}