namespace HansKindberg.VisualStudio.Templates.Wizards.Forms
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