using System;
using System.Collections.Generic;

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

			wizard.Finished += this.OnFinished;
			wizard.Finishing += this.OnFinishing;
			wizard.ProjectGenerationFinished += this.OnProjectGenerationFinished;
			wizard.ProjectGenerationFinishing += this.OnProjectGenerationFinishing;
			wizard.Started += this.OnStarted;
			wizard.Starting += this.OnStarting;
		}

		#endregion

		#region Methods

		protected internal virtual void OnFinished(object sender, EventArgs e) {}
		protected internal virtual void OnFinishing(object sender, WizardEventArgs e) {}

		protected internal virtual void OnProjectGenerationFinished(object sender, ProjectEventArgs e)
		{
			
		}

		protected internal virtual void OnProjectGenerationFinishing(object sender, ProjectResultEventArgs e)
		{
			
		}
		protected internal virtual void OnStarted(object sender, EventArgs e) {}
		protected internal virtual void OnStarting(object sender, WizardEventArgs e) {}

		#endregion
	}
}