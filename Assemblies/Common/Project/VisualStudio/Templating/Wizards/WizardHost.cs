using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class WizardHost : IWizardHost
	{
		#region Constructors

		public WizardHost(IWizardControllerFactory wizardControllerFactory)
		{
			if(wizardControllerFactory == null)
				throw new ArgumentNullException(nameof(wizardControllerFactory));

			this.WizardControllerFactory = wizardControllerFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IWizardControllerFactory WizardControllerFactory { get; }
		protected internal virtual IDictionary<Type, Type> WizardControllerTypeCache { get; } = new Dictionary<Type, Type>();
		protected internal virtual object WizardControllerTypeCacheLock { get; } = new object();

		#endregion

		#region Methods

		protected internal virtual Type GetWizardControllerType(IWizard wizard)
		{
			if(wizard == null)
				throw new ArgumentNullException(nameof(wizard));

			var wizardType = wizard.GetType();
			Type wizardControllerType;

			// ReSharper disable InvertIf
			if(!this.WizardControllerTypeCache.TryGetValue(wizardType, out wizardControllerType))
			{
				lock(this.WizardControllerTypeCacheLock)
				{
					if(!this.WizardControllerTypeCache.TryGetValue(wizardType, out wizardControllerType))
					{
						var wizardControllerAttribute = wizardType.GetCustomAttributes(typeof(WizardControllerAttribute), false).OfType<WizardControllerAttribute>().FirstOrDefault();

						if(wizardControllerAttribute == null || wizardControllerAttribute.WizardControllerType == null)
							throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is no wizard-controller-type registered for wizard-type \"{0}\".", wizardType));

						wizardControllerType = wizardControllerAttribute.WizardControllerType;

						this.WizardControllerTypeCache.Add(wizardType, wizardControllerType);
					}
				}
			}
			// ReSharper restore InvertIf

			return wizardControllerType;
		}

		public virtual void Register(object automationInstance, object[] parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard)
		{
			if(automationInstance == null)
				throw new ArgumentNullException(nameof(automationInstance));

			if(parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			if(replacements == null)
				throw new ArgumentNullException(nameof(replacements));

			if(wizard == null)
				throw new ArgumentNullException(nameof(wizard));

			this.WizardControllerFactory.Create(automationInstance, parameters, replacements, runKind, wizard, this.GetWizardControllerType(wizard));
		}

		#endregion
	}
}