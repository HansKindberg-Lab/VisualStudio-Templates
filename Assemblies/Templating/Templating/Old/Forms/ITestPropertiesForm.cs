namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public interface ITestPropertiesForm : IPropertiesForm
	{
		#region Properties

		string IntegrationTestProjectName { get; set; }
		string ShimTestProjectName { get; set; }
		string UnitTestProjectName { get; set; }

		#endregion
	}
}