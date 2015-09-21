using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templates.Wizards.Environment.Extensions
{
	public static class SolutionExtension
	{
		#region Methods

		public static IEnumerable<Project> GetAllProjects(this Solution solution)
		{
			if(solution == null)
				throw new ArgumentNullException("solution");

			foreach(var project in solution.Projects.Cast<Project>())
			{
				yield return project;

				foreach(var childProject in GetAllProjects(project))
				{
					yield return childProject;
				}
			}
		}

		private static IEnumerable<Project> GetAllProjects(Project project)
		{
			if(project == null)
				throw new ArgumentNullException("project");

			return GetAllProjects(project.ProjectItems);
		}

		private static IEnumerable<Project> GetAllProjects(ProjectItems projectItems)
		{
			if(projectItems == null)
				yield break;

			foreach(var projectItem in projectItems.Cast<ProjectItem>())
			{
				if(projectItem.SubProject != null)
				{
					yield return projectItem.SubProject;

					foreach(var project in GetAllProjects(projectItem.SubProject))
					{
						yield return project;
					}
				}
				else
				{
					foreach(var project in GetAllProjects(projectItem.ProjectItems))
					{
						yield return project;
					}
				}
			}
		}

		#endregion
	}
}