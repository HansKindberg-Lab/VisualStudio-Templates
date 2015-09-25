using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using EnvDTE;
using HansKindberg.VisualStudio.Templates.Wizards.Forms;

namespace HansKindberg.VisualStudio.Templates.Wizards
{
	public class SolutionRootWizard : BasicSolutionWizard
	{
		#region Constructors

		public SolutionRootWizard() : this(new FileSystem(), new MessageBoxFactory()) {}
		public SolutionRootWizard(IFileSystem fileSystem, IMessageBoxFactory messageBoxFactory) : base(fileSystem, messageBoxFactory) {}

		#endregion

		#region Methods

		protected internal override void AddProjectItemsToSolution(IEnumerable<ProjectItem> projectItems, string sourceDirectoryPath, string destinationDirectoryPath)
		{
			if(projectItems == null)
				throw new ArgumentNullException("projectItems");

			foreach(var projectItem in projectItems)
			{
				this.AddProjectItemToSolutionPhysicallyIfNecessary(projectItem, sourceDirectoryPath, destinationDirectoryPath);
			}
		}

		protected internal override string GetDestinationDirectoryPath()
		{
			return this.SolutionDirectoryPath;
		}

		#endregion
	}
}