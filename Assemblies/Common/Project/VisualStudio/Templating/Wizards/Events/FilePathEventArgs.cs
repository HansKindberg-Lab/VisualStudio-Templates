using System;

namespace HansKindberg.VisualStudio.Templating.Wizards.Events
{
	public class FilePathEventArgs : EventArgs
	{
		#region Constructors

		public FilePathEventArgs(string filePath)
		{
			this.FilePath = filePath;
		}

		#endregion

		#region Properties

		public virtual bool Cancel { get; set; }
		public virtual string FilePath { get; }

		#endregion
	}
}