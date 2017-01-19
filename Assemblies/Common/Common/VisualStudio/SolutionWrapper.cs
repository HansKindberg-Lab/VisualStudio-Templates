using System;
using System.Diagnostics.CodeAnalysis;
using EnvDTE;
using HansKindberg.Abstractions;

namespace HansKindberg.VisualStudio
{
	[CLSCompliant(false)]
	public abstract class SolutionWrapper<T> : Wrapper<T>, ISolution where T : Solution
	{
		#region Constructors

		protected SolutionWrapper(T solution, string parameterName) : base(solution, parameterName) {}

		#endregion

		#region Properties

		public virtual string FileName => this.WrappedInstance.FileName;

		#endregion
	}

	[CLSCompliant(false)]
	public class SolutionWrapper : SolutionWrapper<Solution>
	{
		#region Constructors

		public SolutionWrapper(Solution solution) : base(solution, nameof(solution)) {}

		#endregion

		#region Methods

		public static SolutionWrapper FromSolution(Solution solution)
		{
			return solution != null ? new SolutionWrapper(solution) : null;
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Solution ToSolution(SolutionWrapper solutionWrapper)
		{
			return solutionWrapper?.WrappedInstance;
		}

		#endregion
	}
}