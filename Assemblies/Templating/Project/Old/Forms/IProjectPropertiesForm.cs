namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public interface IProjectPropertiesForm : IPropertiesForm
	{
		#region Properties

		string AssemblyName { get; set; }
		string RootNamespace { get; set; }

		#endregion
	}
}