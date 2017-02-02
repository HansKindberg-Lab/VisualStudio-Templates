﻿using System;

namespace HansKindberg.VisualStudio.Templating.Wizards
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