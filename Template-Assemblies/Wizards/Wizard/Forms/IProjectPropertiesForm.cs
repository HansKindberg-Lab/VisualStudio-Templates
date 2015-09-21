namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
{
	public interface IProjectPropertiesForm : IPropertiesForm
	{
		#region Properties

		string AssemblyName { get; set; }
		string RootNamespace { get; set; }

		#endregion
	}
}