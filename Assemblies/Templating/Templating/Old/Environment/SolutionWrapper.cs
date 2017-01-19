using System;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templating.Old.Environment
{
	[CLSCompliant(false)]
	public class SolutionWrapper : IProjectContainer
	{
		#region Constructors

		public SolutionWrapper(Solution solution)
		{
			if(solution == null)
				throw new ArgumentNullException(nameof(solution));

			this.Solution = solution;
		}

		#endregion

		#region Properties

		protected internal virtual Solution Solution { get; }

		#endregion

		#region Methods

		public virtual Project AddFromFile(string projectFilePath)
		{
			return this.Solution.AddFromFile(projectFilePath);
		}

		#endregion
	}
}