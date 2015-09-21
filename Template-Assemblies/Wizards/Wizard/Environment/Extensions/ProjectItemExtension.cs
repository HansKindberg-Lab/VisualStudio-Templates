using System;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templates.Wizards.Environment.Extensions
{
	public static class ProjectItemExtension
	{
		#region Methods

		public static bool IsPhysicalFolder(this ProjectItem projectItem)
		{
			if(projectItem == null)
				throw new ArgumentNullException("projectItem");

			if(string.IsNullOrEmpty(projectItem.Kind))
				return false;

			return string.Equals(projectItem.Kind, Constants.vsProjectItemKindPhysicalFolder, StringComparison.OrdinalIgnoreCase);
		}

		#endregion
	}
}