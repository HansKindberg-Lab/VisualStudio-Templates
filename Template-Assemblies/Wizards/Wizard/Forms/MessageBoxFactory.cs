namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
{
	public class MessageBoxFactory : IMessageBoxFactory
	{
		#region Methods

		public virtual IMessageBox Create()
		{
			return new MessageBoxWrapper();
		}

		#endregion
	}
}