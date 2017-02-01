using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public abstract class BasicWizardController : IWizardController
	{
		#region Constructors

		protected BasicWizardController(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind)
		{
			if(developmentToolsEnvironment == null)
				throw new ArgumentNullException(nameof(developmentToolsEnvironment));

			if(parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			if(replacements == null)
				throw new ArgumentNullException(nameof(replacements));

			this.DevelopmentToolsEnvironment = developmentToolsEnvironment;
			this.Parameters = parameters;
			this.Replacements = replacements;
			this.RunKind = runKind;
		}

		#endregion

		#region Properties

		public virtual IDevelopmentToolsEnvironment DevelopmentToolsEnvironment { get; }
		public virtual IEnumerable<object> Parameters { get; }
		public virtual IDictionary<string, string> Replacements { get; }
		public virtual WizardRunKind RunKind { get; }

		#endregion

		#region Methods

		public abstract void BeforeOpeningFile(IProjectItem projectItem);
		public abstract void ProjectFinishedGenerating(IProject project);
		public abstract void ProjectItemFinishedGenerating(IProjectItem projectItem);
		public abstract void RunFinished();
		public abstract void RunStarted();
		public abstract bool ShouldAddProjectItem(string filePath);

		#endregion
	}
}