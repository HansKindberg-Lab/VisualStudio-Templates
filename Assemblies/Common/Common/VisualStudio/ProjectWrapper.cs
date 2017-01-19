using System;
using System.Diagnostics.CodeAnalysis;
using EnvDTE;
using HansKindberg.Abstractions;

namespace HansKindberg.VisualStudio
{
	[CLSCompliant(false)]
	public abstract class ProjectWrapper<T> : Wrapper<T>, IProject where T : Project
	{
		#region Constructors

		protected ProjectWrapper(T project, string parameterName) : base(project, parameterName) {}

		#endregion

		#region Properties

		public virtual string Name
		{
			get { return this.WrappedInstance.Name; }
			set { this.WrappedInstance.Name = value; }
		}

		#endregion
	}

	[CLSCompliant(false)]
	public class ProjectWrapper : ProjectWrapper<Project>
	{
		#region Constructors

		public ProjectWrapper(Project project) : base(project, nameof(project)) {}

		#endregion

		#region Methods

		public static ProjectWrapper FromProject(Project project)
		{
			return project != null ? new ProjectWrapper(project) : null;
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static Project ToProject(ProjectWrapper projectWrapper)
		{
			return projectWrapper?.WrappedInstance;
		}

		#endregion
	}
}