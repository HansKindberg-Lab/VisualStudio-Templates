using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Linq;
using EnvDTE;
using HansKindberg.VisualStudio.Templates.Wizards.Forms;

namespace HansKindberg.VisualStudio.Templates.Wizards
{
	public abstract class BasicSolutionWizard : Wizard
	{
		#region Fields

		private Lazy<string> _destinationDirectoryPath;

		#endregion

		#region Constructors

		protected BasicSolutionWizard(IFileSystem fileSystem, IMessageBoxFactory messageBoxFactory) : base(fileSystem, messageBoxFactory) {}

		#endregion

		#region Properties

		protected internal override string DestinationDirectoryPath
		{
			get
			{
				if(this._destinationDirectoryPath == null)
					this._destinationDirectoryPath = new Lazy<string>(this.GetDestinationDirectoryPath);

				return this._destinationDirectoryPath.Value;
			}
			set { this._destinationDirectoryPath = new Lazy<string>(() => value); }
		}

		#endregion

		#region Methods

		protected internal virtual void AddProjectItemsToSolution(Project project)
		{
			if(project == null)
				throw new ArgumentNullException("project");

			var sourceDirectoryPath = this.GetSourceDirectoryPath(project);

			this.AddProjectItemsToSolution(project.ProjectItems.Cast<ProjectItem>(), sourceDirectoryPath, this.DestinationDirectoryPath);

			this.FileSystem.Directory.Delete(sourceDirectoryPath, true);
		}

		protected internal abstract void AddProjectItemsToSolution(IEnumerable<ProjectItem> projectItems, string sourceDirectoryPath, string destinationDirectoryPath);

		protected internal virtual void AddProjectItemToSolutionPhysicallyIfNecessary(ProjectItem projectItem, string sourceDirectoryPath, string destinationDirectoryPath)
		{
			if(projectItem == null)
				throw new ArgumentNullException("projectItem");

			var sourceFilePath = this.FileSystem.Path.Combine(sourceDirectoryPath, projectItem.Name);

			if(!this.FileSystem.File.Exists(sourceFilePath))
				return;

			var destinationFilePath = this.FileSystem.Path.Combine(destinationDirectoryPath, projectItem.Name);

			if(this.FileSystem.File.Exists(destinationFilePath))
				return;

			if(!this.FileSystem.Directory.Exists(destinationDirectoryPath))
				this.FileSystem.Directory.CreateDirectory(destinationDirectoryPath);

			this.FileSystem.File.Copy(sourceFilePath, destinationFilePath, false);
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected internal abstract string GetDestinationDirectoryPath();

		protected internal virtual string GetSourceDirectoryPath(Project project)
		{
			if(project == null)
				throw new ArgumentNullException("project");

			return this.FileSystem.FileInfo.FromFileName(project.FullName).Directory.FullName;
		}

		public override void ProjectFinishedGenerating(Project project)
		{
			try
			{
				this.AddProjectItemsToSolution(project);

				this.Solution.Remove(project);
			}
			catch(Exception exception)
			{
				this.HandleException(exception);

				this.Cancel();
			}
		}

		protected internal override void Run() {}

		#endregion
	}
}