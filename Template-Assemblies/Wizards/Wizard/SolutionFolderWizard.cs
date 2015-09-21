using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using HansKindberg.VisualStudio.Templates.Wizards.Environment.Extensions;
using HansKindberg.VisualStudio.Templates.Wizards.Forms;
using HansKindberg.VisualStudio.Templates.Wizards.IO.Extensions;

namespace HansKindberg.VisualStudio.Templates.Wizards
{
	public class SolutionFolderWizard : BasicSolutionWizard
	{
		#region Fields

		private const string _solutionFolderNameKey = "$solution-folder-name$";

		#endregion

		#region Constructors

		public SolutionFolderWizard() : this(new FileSystem(), new MessageBoxFactory()) {}
		public SolutionFolderWizard(IFileSystem fileSystem, IMessageBoxFactory messageBoxFactory) : base(fileSystem, messageBoxFactory) {}

		#endregion

		#region Properties

		protected internal virtual string SolutionFolderName
		{
			get { return this.GetReplacementValue(_solutionFolderNameKey); }
		}

		#endregion

		#region Methods

		protected internal virtual void ConvertProjectToSolutionFolder()
		{
			var project = this.GetProject();

			var solutionFolderSourcePath = this.GetSourceDirectoryPath(project);

			var solutionFolder = this.GetSolutionFolder();

			if(solutionFolder == null)
			{
				var solution = (Solution2) this.DevelopmentToolsEnvironment.Solution;

				solutionFolder = (SolutionFolder) solution.AddSolutionFolder(this.SolutionFolderName).Object;
			}

			var projectItems = project.ProjectItems.Cast<ProjectItem>();

			this.SynchronizeSolutionFolderContent(solutionFolder, solutionFolderSourcePath, this.FileSystem.Path.Combine(this.SolutionDirectoryPath, solutionFolder.Parent.Name), projectItems);

			this.DevelopmentToolsEnvironment.Solution.Remove(project);

			this.FileSystem.Directory.Delete(solutionFolderSourcePath, true);
		}

		protected internal virtual SolutionFolder GetSolutionFolder()
		{
			return this.GetSolutionFolder(this.SolutionFolderName);
		}

		protected internal virtual SolutionFolder GetSolutionFolder(string solutionFolderName)
		{
			var project = this.GetProject(solutionFolderName);

			if(project != null)
				return project.Object as SolutionFolder;

			return null;
		}

		public override void RunFinished()
		{
			try
			{
				this.ConvertProjectToSolutionFolder();
			}
			catch(Exception exception)
			{
				this.HandleException(exception);
			}
		}

		protected internal virtual void SynchronizeSolutionFolderContent(SolutionFolder solutionFolder, string solutionFolderSourcePath, string solutionFolderDestinationPath, IEnumerable<ProjectItem> projectItems)
		{
			if(solutionFolder == null)
				throw new ArgumentNullException("solutionFolder");

			if(projectItems == null)
				throw new ArgumentNullException("projectItems");

			foreach(var projectItem in projectItems)
			{
				if(projectItem.IsPhysicalFolder())
				{
					this.SynchronizeSolutionFolderContent((SolutionFolder) solutionFolder.AddSolutionFolder(projectItem.Name).Object, this.FileSystem.Path.Combine(solutionFolderSourcePath, projectItem.Name), this.FileSystem.Path.Combine(solutionFolderDestinationPath, projectItem.Name), projectItem.ProjectItems.Cast<ProjectItem>());
				}
				else
				{
					var destinationFilePath = this.FileSystem.Path.Combine(solutionFolderDestinationPath, projectItem.Name);
					var destinationFileExists = this.FileSystem.File.Exists(destinationFilePath);

					var sourceFilePath = this.FileSystem.Path.Combine(solutionFolderSourcePath, projectItem.Name);
					var sourceFileExists = this.FileSystem.File.Exists(sourceFilePath);

					if(!destinationFileExists)
					{
						if(sourceFileExists)
							this.FileSystem.CopyFile(sourceFilePath, this.FileSystem.FileInfo.FromFileName(destinationFilePath).DirectoryName, false, true);
						else
							this.FileSystem.CreateFile(destinationFilePath, false);
					}

					solutionFolder.Parent.ProjectItems.AddFromFile(destinationFilePath);

					if(!destinationFileExists && !sourceFileExists)
						this.FileSystem.File.Delete(destinationFilePath);
				}
			}
		}

		#endregion
	}
}