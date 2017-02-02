using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public interface IWizardHost
	{
		#region Methods

		void Register(object automationInstance, object[] parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard);

		#endregion
	}
}