using EnvDTE;

namespace HansKindberg.VisualStudio.Templates.Wizards.Environment
{
	public interface IProjectContainer
	{
		#region Methods

		Project AddFromFile(string projectFilePath);

		#endregion
	}
}