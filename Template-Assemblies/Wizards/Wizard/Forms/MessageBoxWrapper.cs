using System.Windows.Forms;

namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
{
	public class MessageBoxWrapper : IMessageBox
	{
		#region Methods

		public virtual void ShowError(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
		}

		public virtual DialogResult ShowWarning(string message)
		{
			return MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, 0);
		}

		#endregion
	}
}