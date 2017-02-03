using System;
using System.Collections.Generic;
using EnvDTE;
using HansKindberg.VisualStudio.Templating.Wizards.Events;
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

		public event EventHandler<FilePathEventArgs> AddingProjectItem;
		public event EventHandler<EventArgs> Finished;
		public event EventHandler<ProjectItemEventArgs> OpeningFile;
		public event EventHandler<ProjectEventArgs> ProjectGenerationFinished;
		public event EventHandler<ProjectItemEventArgs> ProjectItemGenerationFinished;
		public event EventHandler<EventArgs> Started;

		#endregion

		#region Properties

		protected internal virtual IWizardHost WizardHost { get; }

		#endregion

		#region Methods

		public virtual void BeforeOpeningFile(ProjectItem projectItem)
		{
			this.OnOpeningFile(new ProjectItemEventArgs(ProjectItemWrapper.FromProjectItem(projectItem)));
		}

		protected internal virtual void OnAddingProjectItem(FilePathEventArgs e)
		{
			this.AddingProjectItem?.Invoke(this, e);
		}

		protected internal virtual void OnFinished(EventArgs e)
		{
			this.Finished?.Invoke(this, e);
		}

		protected internal virtual void OnOpeningFile(ProjectItemEventArgs e)
		{
			this.OpeningFile?.Invoke(this, e);
		}

		protected internal virtual void OnProjectGenerationFinished(ProjectEventArgs e)
		{
			this.ProjectGenerationFinished?.Invoke(this, e);
		}

		protected internal virtual void OnProjectItemGenerationFinished(ProjectItemEventArgs e)
		{
			this.ProjectItemGenerationFinished?.Invoke(this, e);
		}

		protected internal virtual void OnStarted(EventArgs e)
		{
			this.Started?.Invoke(this, e);
		}

		public virtual void ProjectFinishedGenerating(Project project)
		{
			this.OnProjectGenerationFinished(new ProjectEventArgs(ProjectWrapper.FromProject(project)));
		}

		public virtual void ProjectItemFinishedGenerating(ProjectItem projectItem)
		{
			this.OnProjectItemGenerationFinished(new ProjectItemEventArgs(ProjectItemWrapper.FromProjectItem(projectItem)));
		}

		public virtual void RunFinished()
		{
			this.OnFinished(EventArgs.Empty);
		}

		public virtual void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			this.WizardHost.Register(automationObject, customParams, replacementsDictionary, runKind, this);

			this.OnStarted(EventArgs.Empty);
		}

		public virtual bool ShouldAddProjectItem(string filePath)
		{
			var filePathEventArgs = new FilePathEventArgs(filePath);

			this.OnAddingProjectItem(filePathEventArgs);

			return !filePathEventArgs.Cancel;
		}

		#endregion
	}
}