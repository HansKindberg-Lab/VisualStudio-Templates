using System;
using HansKindberg.InversionOfControl;
using HansKindberg.VisualStudio.Templating.InversionOfControl;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public static class WizardControllerFactory
	{
		#region Fields

		private static volatile IWizardControllerFactory _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IWizardControllerFactory Instance
		{
			get
			{
				// ReSharper disable InvertIf
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
						{
							ServiceLocator.Instance = new StaticServiceLocator();

							_instance = ServiceLocator.Instance.GetService<IWizardControllerFactory>();
						}
					}
				}
				// ReSharper restore InvertIf

				return _instance;
			}
			set
			{
				if(value == _instance)
					return;

				lock(_lockObject)
				{
					_instance = value;
				}
			}
		}

		#endregion
	}
}