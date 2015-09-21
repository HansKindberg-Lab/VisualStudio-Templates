using System;
using System.IO.Abstractions;
using EnvDTE;
using HansKindberg.VisualStudio.Templates.Wizards.Forms;

namespace HansKindberg.VisualStudio.Templates.Wizards
{
	public abstract class BasicSolutionWizard : Wizard
	{
		#region Constructors

		protected BasicSolutionWizard(IFileSystem fileSystem, IMessageBoxFactory messageBoxFactory) : base(fileSystem, messageBoxFactory) {}

		#endregion

		#region Methods

		protected internal virtual string GetSourceDirectoryPath(Project project)
		{
			if(project == null)
				throw new ArgumentNullException("project");

			return this.FileSystem.FileInfo.FromFileName(project.FullName).Directory.FullName;
		}

		protected internal override void Run() {}

		#endregion
	}
}