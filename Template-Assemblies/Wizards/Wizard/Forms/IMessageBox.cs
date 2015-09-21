using System.Windows.Forms;

namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
{
	public interface IMessageBox
	{
		#region Methods

		void ShowError(string message);
		DialogResult ShowWarning(string message);

		#endregion
	}
}