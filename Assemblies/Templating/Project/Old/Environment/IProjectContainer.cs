using System;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templating.Old.Environment
{
	[CLSCompliant(false)]
	public interface IProjectContainer
	{
		#region Methods

		Project AddFromFile(string projectFilePath);

		#endregion
	}
}