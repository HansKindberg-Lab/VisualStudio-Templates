using System;
using EnvDTE;
using EnvDTE80;

namespace HansKindberg.VisualStudio.Templating.Old.Environment.Extensions
{
	[CLSCompliant(false)]
	public static class ProjectExtension
	{
		#region Methods

		public static IProjectContainer Parent(this Project project)
		{
			if(project == null)
				throw new ArgumentNullException(nameof(project));

			if(project.ParentProjectItem == null)
				return new SolutionWrapper(project.DTE.Solution);

			return new SolutionFolderWrapper((SolutionFolder) project.ParentProjectItem.Collection.ContainingProject.Object);
		}

		#endregion
	}
}