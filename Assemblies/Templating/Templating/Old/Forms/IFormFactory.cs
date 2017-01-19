namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public interface IFormFactory
	{
		#region Methods

		IProjectPropertiesForm CreateProjectPropertiesForm();
		ITestPropertiesForm CreateTestPropertiesForm();

		#endregion
	}
}