using System;
using System.Collections.Generic;
using System.Globalization;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public abstract class BasicWizardControllerFactory : IWizardControllerFactory
	{
		#region Methods

		public virtual IWizardController Create(object automationInstance, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard, Type wizardControllerType)
		{
			if(automationInstance == null)
				throw new ArgumentNullException(nameof(automationInstance));

			var dte = automationInstance as DTE;

			if(dte == null)
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The automation-instance must be of type \"{0}\".", typeof(DTE)), nameof(automationInstance));

			return this.Create(DteWrapper.FromDte(dte), parameters, replacements, runKind, wizard, wizardControllerType);
		}

		public abstract IWizardController Create(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard, Type wizardControllerType);

		#endregion
	}
}