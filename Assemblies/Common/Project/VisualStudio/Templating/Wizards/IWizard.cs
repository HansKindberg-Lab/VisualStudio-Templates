using System;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	public interface IWizard
	{
		#region Events

		event EventHandler<EventArgs> Finished;
		event EventHandler<WizardEventArgs> Finishing;
		event EventHandler<ProjectEventArgs> ProjectGenerationFinished;
		event EventHandler<ProjectResultEventArgs> ProjectGenerationFinishing;
		event EventHandler<EventArgs> Started;
		event EventHandler<WizardEventArgs> Starting;

		#endregion
	}
}