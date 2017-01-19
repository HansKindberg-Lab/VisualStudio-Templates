using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using HansKindberg.VisualStudio.Templating.Old.Environment.Extensions;
using HansKindberg.VisualStudio.Templating.Old.Forms;

namespace HansKindberg.VisualStudio.Templating.Old
{
	[CLSCompliant(false)]
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

		protected internal override void AddProjectItemsToSolution(IEnumerable<ProjectItem> projectItems, string sourceDirectoryPath, string destinationDirectoryPath)
		{
			this.AddProjectItemsToSolution(this.GetOrCreateSolutionFolder(), projectItems, sourceDirectoryPath, destinationDirectoryPath);
		}

		protected internal virtual void AddProjectItemsToSolution(SolutionFolder solutionFolder, IEnumerable<ProjectItem> projectItems, string sourceDirectoryPath, string destinationDirectoryPath)
		{
			if(solutionFolder == null)
				throw new ArgumentNullException(nameof(solutionFolder));

			if(projectItems == null)
				throw new ArgumentNullException(nameof(projectItems));

			foreach(var projectItem in projectItems)
			{
				if(projectItem.IsPhysicalFolder())
				{
					var childItem = solutionFolder.Parent.ProjectItems.Cast<ProjectItem>().FirstOrDefault(item => string.Equals(item.Name, projectItem.Name, StringComparison.OrdinalIgnoreCase));

					// ReSharper disable ConvertIfStatementToNullCoalescingExpression
					if(childItem == null)
						childItem = solutionFolder.AddSolutionFolder(projectItem.Name).ParentProjectItem;
					// ReSharper restore ConvertIfStatementToNullCoalescingExpression

					SolutionFolder childSolutionFolder = null;

					if(childItem.SubProject != null)
						childSolutionFolder = childItem.SubProject.Object as SolutionFolder;

					if(childSolutionFolder == null)
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The project-item \"{0}\" already exists but is not a solution-folder.", childItem.Name));

					this.AddProjectItemsToSolution(childSolutionFolder, projectItem.ProjectItems.Cast<ProjectItem>(), this.FileSystem.Path.Combine(sourceDirectoryPath, projectItem.Name), this.FileSystem.Path.Combine(destinationDirectoryPath, projectItem.Name));
				}
				else
				{
					this.AddProjectItemToSolutionPhysicallyIfNecessary(projectItem, sourceDirectoryPath, destinationDirectoryPath);

					var itemAlreadyExists = solutionFolder.Parent.ProjectItems.Cast<ProjectItem>().Any(item => string.Equals(item.Name, projectItem.Name, StringComparison.OrdinalIgnoreCase));

					if(itemAlreadyExists)
						continue;

					var filesToDelete = new List<FileInfoBase>();

					var destinationFilePath = this.FileSystem.Path.Combine(destinationDirectoryPath, projectItem.Name);

					if(!this.FileSystem.File.Exists(destinationFilePath))
					{
						var file = this.FileSystem.FileInfo.FromFileName(destinationFilePath);

						if(!file.Directory.Exists)
							file.Directory.Create();

						using(this.FileSystem.File.Create(destinationFilePath)) {}

						filesToDelete.Add(file);
					}

					solutionFolder.Parent.ProjectItems.AddFromFile(destinationFilePath);

					var document = this.DevelopmentToolsEnvironment.Documents.Cast<Document>().FirstOrDefault(item => string.Equals(item.FullName, destinationFilePath, StringComparison.OrdinalIgnoreCase));

					if(document != null)
						document.Close();

					foreach(var item in filesToDelete)
					{
						var directory = item.Directory;

						if(item.Exists)
							item.Delete();

						if(!directory.GetFileSystemInfos().Any())
							directory.Delete();
					}
				}
			}
		}

		protected internal override string GetDestinationDirectoryPath()
		{
			return this.FileSystem.Path.Combine(this.SolutionDirectoryPath, this.SolutionFolderName);
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected internal virtual SolutionFolder GetOrCreateSolutionFolder()
		{
			var project = this.Solution.Projects.Cast<Project>().FirstOrDefault(item => string.Equals(item.Name, this.SolutionFolderName, StringComparison.OrdinalIgnoreCase));

			// ReSharper disable ConvertIfStatementToNullCoalescingExpression
			// ReSharper disable SuspiciousTypeConversion.Global
			if(project == null)
				project = ((Solution2) this.Solution).AddSolutionFolder(this.SolutionFolderName);
			// ReSharper restore SuspiciousTypeConversion.Global
			// ReSharper restore ConvertIfStatementToNullCoalescingExpression

			var solutionFolder = project.Object as SolutionFolder;

			if(solutionFolder == null)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The project \"{0}\" already exists and is not a solution-folder.", this.SolutionFolderName));

			return solutionFolder;
		}

		#endregion
	}
}