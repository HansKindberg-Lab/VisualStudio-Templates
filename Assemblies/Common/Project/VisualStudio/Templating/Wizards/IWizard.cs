using System;
using HansKindberg.VisualStudio.Templating.Wizards.Events;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	public interface IWizard
	{
		#region Events

		event EventHandler<FilePathEventArgs> AddingProjectItem;
		event EventHandler<EventArgs> Finished;
		event EventHandler<ProjectItemEventArgs> OpeningFile;
		event EventHandler<ProjectEventArgs> ProjectGenerationFinished;
		event EventHandler<ProjectItemEventArgs> ProjectItemGenerationFinished;
		event EventHandler<EventArgs> Started;

		#endregion
	}
}