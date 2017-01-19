namespace HansKindberg.VisualStudio.Templating.Wizards
{
	public class WizardController : IWizardController
	{
		#region Methods

		public virtual void BeforeOpeningFile(IProjectItem projectItem) {}
		public virtual void ProjectFinishedGenerating(IProject project) {}
		public virtual void ProjectItemFinishedGenerating(IProjectItem projectItem) {}
		public virtual void RunFinished() {}

		public virtual bool ShouldAddProjectItem(string filePath)
		{
			return true;
		}

		#endregion
	}
}