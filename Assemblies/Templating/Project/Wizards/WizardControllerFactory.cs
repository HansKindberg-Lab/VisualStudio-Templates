using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class WizardControllerFactory : BasicWizardControllerFactory
	{
		#region Methods

		public override IWizardController Create(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard, Type wizardControllerType)
		{
			if(developmentToolsEnvironment == null)
				throw new ArgumentNullException(nameof(developmentToolsEnvironment));

			if(parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			if(replacements == null)
				throw new ArgumentNullException(nameof(replacements));

			if(wizard == null)
				throw new ArgumentNullException(nameof(wizard));

			if(wizardControllerType == null)
				throw new ArgumentNullException(nameof(wizardControllerType));

			return (IWizardController) Activator.CreateInstance(wizardControllerType, developmentToolsEnvironment, parameters, replacements, runKind, wizard);
		}

		#endregion
	}
}