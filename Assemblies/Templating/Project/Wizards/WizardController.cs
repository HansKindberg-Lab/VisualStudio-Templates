using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class WizardController : BasicWizardController
	{
		#region Constructors

		public WizardController(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind) : base(developmentToolsEnvironment, parameters, replacements, runKind) {}

		#endregion

		#region Methods

		public override void BeforeOpeningFile(IProjectItem projectItem)
		{
			
		}

		public override void ProjectFinishedGenerating(IProject project)
		{
			
		}

		public override void ProjectItemFinishedGenerating(IProjectItem projectItem)
		{
			
		}

		public override void RunFinished()
		{
			
		}

		public override void RunStarted()
		{
			
		}

		public override bool ShouldAddProjectItem(string filePath)
		{
			return true;
		}

		#endregion
	}
}