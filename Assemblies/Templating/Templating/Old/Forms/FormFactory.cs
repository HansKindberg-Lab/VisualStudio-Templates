namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public class FormFactory : IFormFactory
	{
		#region Methods

		public virtual IProjectPropertiesForm CreateProjectPropertiesForm()
		{
			return new ProjectPropertiesForm();
		}

		public virtual ITestPropertiesForm CreateTestPropertiesForm()
		{
			return new TestPropertiesForm();
		}

		#endregion
	}
}