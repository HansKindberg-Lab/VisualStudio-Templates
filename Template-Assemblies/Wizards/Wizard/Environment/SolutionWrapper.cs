using System;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templates.Wizards.Environment
{
	public class SolutionWrapper : IProjectContainer
	{
		#region Fields

		private readonly Solution _solution;

		#endregion

		#region Constructors

		public SolutionWrapper(Solution solution)
		{
			if(solution == null)
				throw new ArgumentNullException("solution");

			this._solution = solution;
		}

		#endregion

		#region Properties

		protected internal virtual Solution Solution
		{
			get { return this._solution; }
		}

		#endregion

		#region Methods

		public virtual Project AddFromFile(string projectFilePath)
		{
			return this.Solution.AddFromFile(projectFilePath);
		}

		#endregion
	}
}