using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using EnvDTE;
using HansKindberg.VisualStudio.Templates.Wizards.Environment.Extensions;
using HansKindberg.VisualStudio.Templates.Wizards.Forms;
using HansKindberg.VisualStudio.Templates.Wizards.IO.Extensions;

namespace HansKindberg.VisualStudio.Templates.Wizards
{
	public class SolutionRootWizard : BasicSolutionWizard
	{
		#region Constructors

		public SolutionRootWizard() : this(new FileSystem(), new MessageBoxFactory()) {}
		public SolutionRootWizard(IFileSystem fileSystem, IMessageBoxFactory messageBoxFactory) : base(fileSystem, messageBoxFactory) {}

		#endregion

		#region Methods

		protected internal virtual void CopyProjectFilesToSolutionRoot()
		{
			var project = this.GetProject();

			var sourceDirectoryPath = this.GetSourceDirectoryPath(project);

			var projectItems = project.ProjectItems.Cast<ProjectItem>();

			this.SynchronizeSolutionRootContent(sourceDirectoryPath, this.SolutionDirectoryPath, projectItems);

			this.DevelopmentToolsEnvironment.Solution.Remove(project);

			this.FileSystem.Directory.Delete(sourceDirectoryPath, true);
		}

		public override void RunFinished()
		{
			try
			{
				this.CopyProjectFilesToSolutionRoot();
			}
			catch(Exception exception)
			{
				this.HandleException(exception);
			}
		}

		protected internal virtual void SynchronizeSolutionRootContent(string sourceDirectoryPath, string solutionRootDestinationPath, IEnumerable<ProjectItem> projectItems)
		{
			if(projectItems == null)
				throw new ArgumentNullException("projectItems");

			foreach(var projectItem in projectItems)
			{
				if(projectItem.IsPhysicalFolder())
				{
					this.SynchronizeSolutionRootContent(this.FileSystem.Path.Combine(sourceDirectoryPath, projectItem.Name), this.FileSystem.Path.Combine(solutionRootDestinationPath, projectItem.Name), projectItem.ProjectItems.Cast<ProjectItem>());
				}
				else
				{
					var destinationFilePath = this.FileSystem.Path.Combine(solutionRootDestinationPath, projectItem.Name);
					var sourceFilePath = this.FileSystem.Path.Combine(sourceDirectoryPath, projectItem.Name);

					this.FileSystem.CopyFile(sourceFilePath, this.FileSystem.FileInfo.FromFileName(destinationFilePath).DirectoryName, false, true);
				}
			}
		}

		#endregion
	}
}