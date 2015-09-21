using System;
using EnvDTE;
using EnvDTE80;

namespace HansKindberg.VisualStudio.Templates.Wizards.Environment
{
	public class SolutionFolderWrapper : IProjectContainer
	{
		#region Fields

		private readonly SolutionFolder _solutionFolder;

		#endregion

		#region Constructors

		public SolutionFolderWrapper(SolutionFolder solutionFolder)
		{
			if(solutionFolder == null)
				throw new ArgumentNullException("solutionFolder");

			this._solutionFolder = solutionFolder;
		}

		#endregion

		#region Properties

		protected internal virtual SolutionFolder SolutionFolder
		{
			get { return this._solutionFolder; }
		}

		#endregion

		#region Methods

		public virtual Project AddFromFile(string projectFilePath)
		{
			return this.SolutionFolder.AddFromFile(projectFilePath);
		}

		#endregion
	}
}