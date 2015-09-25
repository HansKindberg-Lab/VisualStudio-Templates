using System;
using System.IO.Abstractions;
using EnvDTE;
using HansKindberg.VisualStudio.Templates.Wizards.Forms;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templates.Wizards
{
	public class ProjectWizard : BasicProjectWizard
	{
		#region Constructors

		public ProjectWizard() : this(new FileSystem(), new FormFactory(), new MessageBoxFactory()) {}
		public ProjectWizard(IFileSystem fileSystem, IFormFactory formFactory, IMessageBoxFactory messageBoxFactory) : base(fileSystem, formFactory, messageBoxFactory) {}

		#endregion

		#region Methods

		public override void ProjectFinishedGenerating(Project project)
		{
			try
			{
				if(this.WizardRunKind != WizardRunKind.AsMultiProject)
					return;

				if(project == null)
					project = this.GetProject(this.ProjectName);

				this.MoveProjectOneDirectoryUp(project);
			}
			catch(Exception exception)
			{
				this.HandleException(exception);
			}
		}

		protected internal override void Run()
		{
			if(!this.ShowProjectPropertiesForm())
				this.Cancel();
		}

		#endregion
	}
}