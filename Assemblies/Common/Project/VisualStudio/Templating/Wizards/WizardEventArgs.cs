using System;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	public class WizardEventArgs : EventArgs
	{
		#region Properties

		public virtual string Message { get; set; }
		public virtual WizardEventResultType Result { get; set; }

		#endregion
	}
}