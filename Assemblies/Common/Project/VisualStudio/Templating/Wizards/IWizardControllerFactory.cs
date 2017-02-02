using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public interface IWizardControllerFactory
	{
		#region Methods

		IWizardController Create(object automationInstance, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard, Type wizardControllerType);

		#endregion
	}
}