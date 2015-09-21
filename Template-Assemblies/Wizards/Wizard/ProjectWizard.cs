using System;
using System.IO.Abstractions;
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

		protected internal override void Run()
		{
			if(!this.ShowProjectPropertiesForm())
				this.Cancel();
		}

		public override void RunFinished()
		{
			try
			{
				if(this.WizardRunKind == WizardRunKind.AsMultiProject)
					this.MoveProjectOneDirectoryUp(this.GetProject());
			}
			catch(Exception exception)
			{
				this.HandleException(exception);
			}
		}

		#endregion
	}
}