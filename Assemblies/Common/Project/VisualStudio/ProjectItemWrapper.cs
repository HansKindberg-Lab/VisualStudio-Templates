using System;
using System.Diagnostics.CodeAnalysis;
using EnvDTE;
using HansKindberg.Abstractions;

namespace HansKindberg.VisualStudio
{
	[CLSCompliant(false)]
	public abstract class ProjectItemWrapper<T> : Wrapper<T>, IProjectItem where T : ProjectItem
	{
		#region Constructors

		protected ProjectItemWrapper(T projectItem, string parameterName) : base(projectItem, parameterName) {}

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
	public class ProjectItemWrapper : ProjectItemWrapper<ProjectItem>
	{
		#region Constructors

		public ProjectItemWrapper(ProjectItem projectItem) : base(projectItem, nameof(projectItem)) {}

		#endregion

		#region Methods

		public static ProjectItemWrapper FromProjectItem(ProjectItem projectItem)
		{
			return projectItem != null ? new ProjectItemWrapper(projectItem) : null;
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static ProjectItem ToProjectItem(ProjectItemWrapper projectItemWrapper)
		{
			return projectItemWrapper?.WrappedInstance;
		}

		#endregion
	}
}