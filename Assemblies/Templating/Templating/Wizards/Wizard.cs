using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class Wizard : IWizard
	{
		#region Fields

		private IWizardController _wizardController;

		#endregion

		#region Constructors

		public Wizard() : this(Wizards.WizardControllerFactory.Instance) {}

		public Wizard(IWizardControllerFactory wizardControllerFactory)
		{
			if(wizardControllerFactory == null)
				throw new ArgumentNullException(nameof(wizardControllerFactory));

			this.WizardControllerFactory = wizardControllerFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IWizardController WizardController
		{
			get
			{
				if(this._wizardController == null)
					throw new InvalidOperationException("The wizard-controller is not set.");

				return this._wizardController;
			}
			set { this._wizardController = value; }
		}

		protected internal virtual IWizardControllerFactory WizardControllerFactory { get; }

		#endregion

		#region Methods

		public virtual void BeforeOpeningFile(ProjectItem projectItem)
		{
			this.WizardController.BeforeOpeningFile(ProjectItemWrapper.FromProjectItem(projectItem));
		}

		public virtual void ProjectFinishedGenerating(Project project)
		{
			this.WizardController.ProjectFinishedGenerating(ProjectWrapper.FromProject(project));
		}

		public virtual void ProjectItemFinishedGenerating(ProjectItem projectItem)
		{
			this.WizardController.ProjectItemFinishedGenerating(ProjectItemWrapper.FromProjectItem(projectItem));
		}

		public virtual void RunFinished()
		{
			this.WizardController.RunFinished();
		}

		public virtual void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			this.WizardController = this.WizardControllerFactory.Create(automationObject, replacementsDictionary, runKind, customParams);
		}

		public virtual bool ShouldAddProjectItem(string filePath)
		{
			return this.WizardController.ShouldAddProjectItem(filePath);
		}

		#endregion
	}
}