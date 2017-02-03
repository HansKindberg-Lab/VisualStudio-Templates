using System;
using System.Collections.Generic;
using HansKindberg.VisualStudio.Templating.Wizards.Events;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class WizardController : BasicWizardController
	{
		#region Constructors

		public WizardController(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, Microsoft.VisualStudio.TemplateWizard.WizardRunKind runKind, IWizard wizard) : base(developmentToolsEnvironment, parameters, replacements, runKind, wizard)
		{
			if(wizard == null)
				throw new ArgumentNullException(nameof(wizard));

			wizard.AddingProjectItem += this.OnAddingProjectItem;
			wizard.Finished += this.OnFinished;
			wizard.OpeningFile += this.OnOpeningFile;
			wizard.ProjectGenerationFinished += this.OnProjectGenerationFinished;
			wizard.ProjectItemGenerationFinished += this.OnProjectItemGenerationFinished;
			wizard.Started += this.OnStarted;
		}

		#endregion

		#region Methods

		protected internal virtual void OnAddingProjectItem(object sender, FilePathEventArgs e) {}
		protected internal virtual void OnFinished(object sender, EventArgs e) {}
		protected internal virtual void OnOpeningFile(object sender, ProjectItemEventArgs e) {}
		protected internal virtual void OnProjectGenerationFinished(object sender, ProjectEventArgs e) {}
		protected internal virtual void OnProjectItemGenerationFinished(object sender, ProjectItemEventArgs e) {}
		protected internal virtual void OnStarted(object sender, EventArgs e) {}

		#endregion
	}
}