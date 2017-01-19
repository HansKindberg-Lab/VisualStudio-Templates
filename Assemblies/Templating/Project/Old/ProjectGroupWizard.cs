using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using HansKindberg.VisualStudio.Templating.Old.Environment.Extensions;
using HansKindberg.VisualStudio.Templating.Old.Forms;
using Microsoft.VisualStudio.TemplateWizard;
using VSLangProj;

namespace HansKindberg.VisualStudio.Templating.Old
{
	[CLSCompliant(false)]
	public class ProjectGroupWizard : BasicProjectWizard
	{
		#region Fields

		private const string _integrationTestAssemblyName = "$integration-test-assembly-name$";
		private const string _integrationTestProjectNameKey = "$integration-test-project-name$";
		private const string _integrationTestProjectNameSuffixKey = "$integration-test-project-name-suffix$";
		private const string _shimTestAssemblyName = "$shim-test-assembly-name$";
		private const string _shimTestProjectNameKey = "$shim-test-project-name$";
		private const string _shimTestProjectNameSuffixKey = "$shim-test-project-name-suffix$";
		private const string _unitTestAssemblyName = "$unit-test-assembly-name$";
		private const string _unitTestProjectNameKey = "$unit-test-project-name$";
		private const string _unitTestProjectNameSuffixKey = "$unit-test-project-name-suffix$";

		#endregion

		#region Constructors

		public ProjectGroupWizard() : this(new FileSystem(), new FormFactory(), new MessageBoxFactory()) {}
		public ProjectGroupWizard(IFileSystem fileSystem, IFormFactory formFactory, IMessageBoxFactory messageBoxFactory) : base(fileSystem, formFactory, messageBoxFactory) {}

		#endregion

		#region Properties

		protected internal virtual string IntegrationTestProjectName
		{
			get
			{
				if(string.IsNullOrWhiteSpace(this.GetReplacementValue(_integrationTestProjectNameKey)))
					this.SetReplacementValue(_integrationTestProjectNameKey, this.CreateTestProjectName(this.IntegrationTestProjectNameSuffix));

				return this.GetReplacementValue(_integrationTestProjectNameKey);
			}
			set { this.SetReplacementValue(_integrationTestProjectNameKey, value); }
		}

		public virtual string IntegrationTestProjectNameSuffix
		{
			get { return this.GetReplacementValue(_integrationTestProjectNameSuffixKey); }
		}

		protected internal virtual string ShimTestProjectName
		{
			get
			{
				if(string.IsNullOrWhiteSpace(this.GetReplacementValue(_shimTestProjectNameKey)))
					this.SetReplacementValue(_shimTestProjectNameKey, this.CreateTestProjectName(this.ShimTestProjectNameSuffix));

				return this.GetReplacementValue(_shimTestProjectNameKey);
			}
			set { this.SetReplacementValue(_shimTestProjectNameKey, value); }
		}

		public virtual string ShimTestProjectNameSuffix
		{
			get { return this.GetReplacementValue(_shimTestProjectNameSuffixKey); }
		}

		protected internal virtual string UnitTestProjectName
		{
			get
			{
				if(string.IsNullOrWhiteSpace(this.GetReplacementValue(_unitTestProjectNameKey)))
					this.SetReplacementValue(_unitTestProjectNameKey, this.CreateTestProjectName(this.UnitTestProjectNameSuffix));

				return this.GetReplacementValue(_unitTestProjectNameKey);
			}
			set { this.SetReplacementValue(_unitTestProjectNameKey, value); }
		}

		public virtual string UnitTestProjectNameSuffix
		{
			get { return this.GetReplacementValue(_unitTestProjectNameSuffixKey); }
		}

		#endregion

		#region Methods

		protected internal virtual string CreateTestProjectName(string testNameSuffix)
		{
			return this.ConcatenateNamespaceParts(this.ProjectName, testNameSuffix, true);
		}

		protected internal virtual ITestPropertiesForm CreateTestPropertiesForm()
		{
			return this.FormFactory.CreateTestPropertiesForm();
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected internal virtual IEnumerable<string> GetTestProjectNames()
		{
			return new[] {this.IntegrationTestProjectName, this.ShimTestProjectName, this.UnitTestProjectName};
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected internal virtual IEnumerable<Project> GetTestProjects()
		{
			return this.GetTestProjectNames().Select(this.GetProject).Where(testProject => testProject != null).ToArray();
		}

		public override void ProjectFinishedGenerating(Project project)
		{
			try
			{
				if(this.WizardRunKind != WizardRunKind.AsMultiProject)
					return;

				if(project == null)
					project = this.GetProject(this.ProjectName);

				// We need to move the test-projects before the project itself, to get rid of directories.
				var testProjects = this.GetTestProjects().Select(this.MoveProjectOneDirectoryUp).ToArray();

				project = this.MoveProjectOneDirectoryUp(project);

				foreach(var testProject in testProjects)
				{
					var testProjectFilePath = testProject.FullName;

					var projectContainer = project.Parent();

					// We remove them and add them again to get them added after the main-project, see variable "project" above.
					this.Solution.Remove(testProject);

					var refreshedTestProject = projectContainer.AddFromFile(testProjectFilePath);

					((VSProject) refreshedTestProject.Object).References.AddProject(project);
				}
			}
			catch(Exception exception)
			{
				this.HandleException(exception);
			}
		}

		protected internal override void Run()
		{
			while(true)
			{
				if(this.ShowProjectPropertiesForm())
				{
					if(this.ShowTestPropertiesForm())
						break;
				}
				else
				{
					this.Cancel();

					break;
				}
			}

			this.Replacements[_integrationTestAssemblyName] = this.AssemblyName + "." + this.IntegrationTestProjectNameSuffix;
			this.Replacements[_shimTestAssemblyName] = this.AssemblyName + "." + this.ShimTestProjectNameSuffix;
			this.Replacements[_unitTestAssemblyName] = this.AssemblyName + "." + this.UnitTestProjectNameSuffix;
		}

		protected internal virtual bool ShowTestPropertiesForm()
		{
			using(var testPropertiesForm = this.CreateTestPropertiesForm())
			{
				testPropertiesForm.IntegrationTestProjectName = this.IntegrationTestProjectName;
				testPropertiesForm.ShimTestProjectName = this.ShimTestProjectName;
				testPropertiesForm.UnitTestProjectName = this.UnitTestProjectName;
				testPropertiesForm.Heading = this.WizardHeading + " - " + this.ProjectName;

				return this.ShowTestPropertiesFormDialog(testPropertiesForm);
			}
		}

		protected internal virtual bool ShowTestPropertiesFormDialog(ITestPropertiesForm testPropertiesForm)
		{
			if(testPropertiesForm == null)
				throw new ArgumentNullException(nameof(testPropertiesForm));

			var dialogResult = testPropertiesForm.ShowDialog();

			if(dialogResult == DialogResult.OK)
			{
				if(string.IsNullOrWhiteSpace(testPropertiesForm.IntegrationTestProjectName) || string.IsNullOrWhiteSpace(testPropertiesForm.ShimTestProjectName) || string.IsNullOrEmpty(testPropertiesForm.UnitTestProjectName))
				{
					this.MessageBoxFactory.Create().ShowError("You must enter a value for \"IntegrationTestProject-name\", \"ShimTestProject-name\" and \"UnitTestProject-name\".");

					return this.ShowTestPropertiesFormDialog(testPropertiesForm);
				}

				this.IntegrationTestProjectName = testPropertiesForm.IntegrationTestProjectName;
				this.ShimTestProjectName = testPropertiesForm.ShimTestProjectName;
				this.UnitTestProjectName = testPropertiesForm.UnitTestProjectName;

				return true;
			}

			return false;
		}

		#endregion
	}
}