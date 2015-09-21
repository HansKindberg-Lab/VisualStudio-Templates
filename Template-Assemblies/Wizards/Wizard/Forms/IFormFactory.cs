namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
{
	public interface IFormFactory
	{
		#region Methods

		IProjectPropertiesForm CreateProjectPropertiesForm();
		ITestPropertiesForm CreateTestPropertiesForm();

		#endregion
	}
}