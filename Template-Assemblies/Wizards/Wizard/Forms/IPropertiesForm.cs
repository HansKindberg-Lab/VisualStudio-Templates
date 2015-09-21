using System;
using System.Windows.Forms;

namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
{
	public interface IPropertiesForm : IDisposable
	{
		#region Properties

		string Heading { get; set; }

		#endregion

		#region Methods

		DialogResult ShowDialog();

		#endregion
	}
}