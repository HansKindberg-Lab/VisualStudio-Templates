using System;

namespace HansKindberg.VisualStudio.Templating.Wizards.Events
{
	public class ProjectEventArgs : EventArgs
	{
		#region Constructors

		public ProjectEventArgs(IProject project)
		{
			this.Project = project;
		}

		#endregion

		#region Properties

		public virtual IProject Project { get; }

		#endregion
	}
}