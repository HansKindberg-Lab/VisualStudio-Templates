namespace HansKindberg.VisualStudio.Templating.Old.Forms
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