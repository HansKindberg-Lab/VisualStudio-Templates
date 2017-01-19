using System.Windows.Forms;

namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public interface IMessageBox
	{
		#region Methods

		void ShowError(string message);
		DialogResult ShowWarning(string message);

		#endregion
	}
}