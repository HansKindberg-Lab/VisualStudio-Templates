using System;

namespace HansKindberg.VisualStudio.Templating.Wizards.Events
{
	public class ProjectItemEventArgs : EventArgs
	{
		#region Constructors

		public ProjectItemEventArgs(IProjectItem projectItem)
		{
			this.ProjectItem = projectItem;
		}

		#endregion

		#region Properties

		public virtual IProjectItem ProjectItem { get; }

		#endregion
	}
}