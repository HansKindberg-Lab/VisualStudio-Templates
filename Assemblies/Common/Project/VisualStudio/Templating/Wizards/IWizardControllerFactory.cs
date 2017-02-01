using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public interface IWizardControllerFactory
	{
		#region Methods

		IWizardController Create(object automationInstance, IDictionary<string, string> replacements, WizardRunKind runKind, object[] parameters);

		#endregion
	}
}