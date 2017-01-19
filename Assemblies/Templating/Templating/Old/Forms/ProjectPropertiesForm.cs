using System.Windows.Forms;

namespace HansKindberg.VisualStudio.Templating.Old.Forms
{
	public partial class ProjectPropertiesForm : Form, IProjectPropertiesForm
	{
		#region Constructors

		public ProjectPropertiesForm()
		{
			this.InitializeComponent();

			this.CancelButton = this._cancelButton;
		}

		#endregion

		#region Properties

		public virtual string AssemblyName
		{
			get { return this._assemblyNameTextBox.Text; }
			set { this._assemblyNameTextBox.Text = value; }
		}

		public virtual string Heading
		{
			get { return this.Text; }
			set { this.Text = value; }
		}

		public virtual string RootNamespace
		{
			get { return this._rootNamespaceTextBox.Text; }
			set { this._rootNamespaceTextBox.Text = value; }
		}

		#endregion
	}
}