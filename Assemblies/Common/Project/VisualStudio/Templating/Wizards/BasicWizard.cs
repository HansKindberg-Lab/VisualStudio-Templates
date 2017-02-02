using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public abstract class BasicWizard : IWizard, Microsoft.VisualStudio.TemplateWizard.IWizard
	{
		#region Constructors

		protected BasicWizard(IWizardHost wizardHost)
		{
			if(wizardHost == null)
				throw new ArgumentNullException(nameof(wizardHost));

			this.WizardHost = wizardHost;
		}

		#endregion

		#region Events

		public event EventHandler<EventArgs> Finished;
		public event EventHandler<WizardEventArgs> Finishing;
		public event EventHandler<ProjectEventArgs> ProjectGenerationFinished;
		public event EventHandler<ProjectResultEventArgs> ProjectGenerationFinishing;
		public event EventHandler<EventArgs> Started;
		public event EventHandler<WizardEventArgs> Starting;

		#endregion

		#region Properties

		protected internal virtual IWizardHost WizardHost { get; }

		#endregion

		#region Methods

		public virtual void BeforeOpeningFile(ProjectItem projectItem)
		{
			//this.WizardController.BeforeOpeningFile(ProjectItemWrapper.FromProjectItem(projectItem));
		}

		protected internal virtual void OnFinished(EventArgs e)
		{
			this.Finished?.Invoke(this, e);
		}

		protected internal virtual void OnFinishing(WizardEventArgs e)
		{
			this.Finishing?.Invoke(this, e);
		}

		protected internal virtual void OnProjectGenerationFinished(ProjectEventArgs e)
		{
			this.ProjectGenerationFinished?.Invoke(this, e);
		}

		protected internal virtual void OnProjectGenerationFinishing(ProjectResultEventArgs e)
		{
			this.ProjectGenerationFinishing?.Invoke(this, e);
		}

		protected internal virtual void OnStarted(EventArgs e)
		{
			this.Started?.Invoke(this, e);
		}

		protected internal virtual void OnStarting(WizardEventArgs e)
		{
			this.Starting?.Invoke(this, e);
		}

		public virtual void ProjectFinishedGenerating(Project project)
		{
			var projectResultEventArguments = new ProjectResultEventArgs(ProjectWrapper.FromProject(project));

			this.OnProjectGenerationFinishing(projectResultEventArguments);

			if(projectResultEventArguments.Result == WizardEventResultType.BackOut)
				throw new WizardBackoutException(projectResultEventArguments.Message);

			if(projectResultEventArguments.Result == WizardEventResultType.Cancel)
				throw new WizardCancelledException(projectResultEventArguments.Message);

			this.OnProjectGenerationFinished(new ProjectEventArgs(projectResultEventArguments.Project));
		}

		public virtual void ProjectItemFinishedGenerating(ProjectItem projectItem)
		{
			//this.WizardController.ProjectItemFinishedGenerating(ProjectItemWrapper.FromProjectItem(projectItem));
		}

		public virtual void RunFinished()
		{
			var wizardEventArguments = new WizardEventArgs();

			this.OnFinishing(wizardEventArguments);

			if(wizardEventArguments.Result == WizardEventResultType.BackOut)
				throw new WizardBackoutException(wizardEventArguments.Message);

			if(wizardEventArguments.Result == WizardEventResultType.Cancel)
				throw new WizardCancelledException(wizardEventArguments.Message);

			this.OnFinished(EventArgs.Empty);
		}

		public virtual void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			this.WizardHost.Register(automationObject, customParams, replacementsDictionary, runKind, this);

			var wizardEventArguments = new WizardEventArgs();

			this.OnStarting(wizardEventArguments);

			if(wizardEventArguments.Result == WizardEventResultType.BackOut)
				throw new WizardBackoutException(wizardEventArguments.Message);

			if(wizardEventArguments.Result == WizardEventResultType.Cancel)
				throw new WizardCancelledException(wizardEventArguments.Message);

			this.OnStarted(EventArgs.Empty);
		}

		public virtual bool ShouldAddProjectItem(string filePath)
		{
			return true;
			//return this.WizardController.ShouldAddProjectItem(filePath);
		}

		#endregion
	}
}