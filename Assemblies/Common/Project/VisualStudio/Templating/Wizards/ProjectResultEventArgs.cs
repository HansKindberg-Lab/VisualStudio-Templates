namespace HansKindberg.VisualStudio.Templating.Wizards
{
	public class ProjectResultEventArgs : WizardEventArgs
	{
		#region Constructors

		public ProjectResultEventArgs(IProject project)
		{
			this.Project = project;
		}

		#endregion

		#region Properties

		public virtual IProject Project { get; }

		#endregion
	}
}