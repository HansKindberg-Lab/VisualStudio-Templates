using System.Windows.Forms;

namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public partial class TestPropertiesForm : Form, ITestPropertiesForm
	{
		#region Constructors

		public TestPropertiesForm()
		{
			this.InitializeComponent();

			this.CancelButton = this._cancelButton;
		}

		#endregion

		#region Properties

		public virtual string Heading
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		public virtual string IntegrationTestProjectName
		{
			get { return this._integrationTestProjectNameTextBox.Text; }
			set { this._integrationTestProjectNameTextBox.Text = value; }
		}

		public virtual string ShimTestProjectName
		{
			get { return this._shimTestProjectNameTextBox.Text; }
			set { this._shimTestProjectNameTextBox.Text = value; }
		}

		public virtual string UnitTestProjectName
		{
			get { return this._unitTestProjectNameTextBox.Text; }
			set { this._unitTestProjectNameTextBox.Text = value; }
		}

		#endregion
	}
}