using System;
using EnvDTE;
using EnvDTE80;

namespace HansKindberg.VisualStudio.Templating.Old.Environment
{
	[CLSCompliant(false)]
	public class SolutionFolderWrapper : IProjectContainer
	{
		#region Constructors

		public SolutionFolderWrapper(SolutionFolder solutionFolder)
		{
			if(solutionFolder == null)
				throw new ArgumentNullException(nameof(solutionFolder));

			this.SolutionFolder = solutionFolder;
		}

		#endregion

		#region Properties

		protected internal virtual SolutionFolder SolutionFolder { get; }

		#endregion

		#region Methods

		public virtual Project AddFromFile(string projectFilePath)
		{
			return this.SolutionFolder.AddFromFile(projectFilePath);
		}

		#endregion
	}
}