using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public interface IWizardController
	{
		#region Properties

		IDevelopmentToolsEnvironment DevelopmentToolsEnvironment { get; }
		IEnumerable<object> Parameters { get; }
		IDictionary<string, string> Replacements { get; }
		WizardRunKind RunKind { get; }
		IWizard Wizard { get; }

		#endregion
	}
}