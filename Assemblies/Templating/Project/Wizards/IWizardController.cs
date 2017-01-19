namespace HansKindberg.VisualStudio.Templating.Wizards
{
	public interface IWizardController
	{
		#region Methods

		void BeforeOpeningFile(IProjectItem projectItem);
		void ProjectFinishedGenerating(IProject project);
		void ProjectItemFinishedGenerating(IProjectItem projectItem);
		void RunFinished();
		bool ShouldAddProjectItem(string filePath);

		#endregion
	}
}