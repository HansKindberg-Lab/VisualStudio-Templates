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

		#endregion

		#region Methods

		void BeforeOpeningFile(IProjectItem projectItem);
		void ProjectFinishedGenerating(IProject project);
		void ProjectItemFinishedGenerating(IProjectItem projectItem);
		void RunFinished();
		void RunStarted();
		bool ShouldAddProjectItem(string filePath);

		#endregion
	}
}