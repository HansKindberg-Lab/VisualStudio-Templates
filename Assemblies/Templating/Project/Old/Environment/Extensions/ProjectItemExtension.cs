using System;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templating.Old.Environment.Extensions
{
	[CLSCompliant(false)]
	public static class ProjectItemExtension
	{
		#region Methods

		public static bool IsPhysicalFolder(this ProjectItem projectItem)
		{
			if(projectItem == null)
				throw new ArgumentNullException(nameof(projectItem));

			if(string.IsNullOrEmpty(projectItem.Kind))
				return false;

			return string.Equals(projectItem.Kind, Constants.vsProjectItemKindPhysicalFolder, StringComparison.OrdinalIgnoreCase);
		}

		#endregion
	}
}