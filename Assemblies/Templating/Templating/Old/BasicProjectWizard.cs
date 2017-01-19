using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using HansKindberg.VisualStudio.Templating.Old.Environment.Extensions;
using HansKindberg.VisualStudio.Templating.Old.Forms;
using Microsoft.VisualStudio.TemplateWizard;
using SystemEnvironment = System.Environment;

namespace HansKindberg.VisualStudio.Templating.Old
{
	[CLSCompliant(false)]
	public abstract class BasicProjectWizard : Wizard
	{
		#region Fields

		private const string _assemblyNameKey = "$assembly-name$";
		private const string _rootNamespaceKey = "$root-namespace$";
		private const string _safeCompanyNameKey = "$safe-company-name$";
		private const string _wizardHeadingKey = "$wizard-heading$";

		#endregion

		#region Constructors

		protected BasicProjectWizard(IFileSystem fileSystem, IFormFactory formFactory, IMessageBoxFactory messageBoxFactory) : base(fileSystem, messageBoxFactory)
		{
			if(formFactory == null)
				throw new ArgumentNullException(nameof(formFactory));

			this.FormFactory = formFactory;
		}

		#endregion

		#region Properties

		protected internal virtual string AssemblyName
		{
			get
			{
				if(string.IsNullOrWhiteSpace(this.GetReplacementValue(_assemblyNameKey)))
					this.SetReplacementValue(_assemblyNameKey, this.CreateCompanyNamespace(this.SafeProjectName));

				return this.GetReplacementValue(_assemblyNameKey);
			}
			set { this.SetReplacementValue(_assemblyNameKey, value); }
		}

		protected internal virtual IFormFactory FormFactory { get; }

		protected internal virtual string RootNamespace
		{
			get
			{
				if(string.IsNullOrWhiteSpace(this.GetReplacementValue(_rootNamespaceKey)))
					this.SetReplacementValue(_rootNamespaceKey, this.CreateCompanyNamespace(this.SafeProjectName));

				return this.GetReplacementValue(_rootNamespaceKey);
			}
			set { this.SetReplacementValue(_rootNamespaceKey, value); }
		}

		protected internal virtual string SafeCompanyName
		{
			get { return this.GetReplacementValue(_safeCompanyNameKey); }
			set { this.SetReplacementValue(_safeCompanyNameKey, value); }
		}

		protected internal virtual string WizardHeading
		{
			get { return this.GetReplacementValue(_wizardHeadingKey); }
		}

		#endregion

		#region Methods

		protected internal override void Cancel()
		{
			this.Cancel(true);
		}

		protected internal virtual void Cancel(bool rollback)
		{
			if(rollback)
				this.Rollback();

			throw new WizardBackoutException();
		}

		protected internal virtual string ConcatenateNamespaceParts(IEnumerable<string> namespaceParts)
		{
			return this.ConcatenateNamespaceParts(namespaceParts, false);
		}

		protected internal virtual string ConcatenateNamespaceParts(IEnumerable<string> namespaceParts, bool removeSubsequentDuplicate)
		{
			var fixedNamespaceParts = new List<string>();

			foreach(var namespacePart in namespaceParts ?? Enumerable.Empty<string>())
			{
				var fixedNamespacePart = (namespacePart ?? string.Empty).Trim(".".ToCharArray());

				if(!string.IsNullOrWhiteSpace(fixedNamespacePart))
				{
					var add = true;

					if(removeSubsequentDuplicate && fixedNamespaceParts.Any())
					{
						if(fixedNamespaceParts.Last().Equals(fixedNamespacePart.Split(".".ToCharArray()).First(), StringComparison.OrdinalIgnoreCase))
							add = false;
					}

					if(add)
						fixedNamespaceParts.Add(fixedNamespacePart);
				}
			}

			return string.Join(".", fixedNamespaceParts);
		}

		protected internal virtual string ConcatenateNamespaceParts(string firstNamespacePart, string secondNamespacePart)
		{
			return this.ConcatenateNamespaceParts(firstNamespacePart, secondNamespacePart, false);
		}

		protected internal virtual string ConcatenateNamespaceParts(string firstNamespacePart, string secondNamespacePart, bool removeSubsequentDuplicate)
		{
			return this.ConcatenateNamespaceParts(new[] {firstNamespacePart, secondNamespacePart});
		}

		protected internal virtual string CreateCompanyNamespace(string namespaceName)
		{
			return this.ConcatenateNamespaceParts(this.SafeCompanyName, namespaceName, true);
		}

		protected internal virtual IProjectPropertiesForm CreateProjectPropertiesForm()
		{
			return this.FormFactory.CreateProjectPropertiesForm();
		}

		protected internal virtual Project MoveProject(Project project, string destinationDirectory)
		{
			if(project == null)
				throw new ArgumentNullException(nameof(project));

			var projectFile = this.FileSystem.FileInfo.FromFileName(project.FullName);

			var sourceDirectory = projectFile.DirectoryName;

			var projectContainer = project.Parent();

			this.Solution.Remove(project);

			if(this.FileSystem.Directory.Exists(destinationDirectory))
			{
				var destinationDirectoryParent = this.FileSystem.DirectoryInfo.FromDirectoryName(destinationDirectory).Parent.FullName;

				var temporaryDestinationDirectory = this.FileSystem.Path.Combine(destinationDirectoryParent, Guid.NewGuid().ToString());

				this.FileSystem.Directory.Move(sourceDirectory, temporaryDestinationDirectory);

				sourceDirectory = temporaryDestinationDirectory;

				this.FileSystem.Directory.Delete(destinationDirectory);
			}

			this.FileSystem.Directory.Move(sourceDirectory, destinationDirectory);

			var projectFileDestinationPath = this.FileSystem.Path.Combine(destinationDirectory, projectFile.Name);

			return projectContainer.AddFromFile(projectFileDestinationPath);
		}

		protected internal virtual Project MoveProjectOneDirectoryUp(Project project)
		{
			if(project == null)
				throw new ArgumentNullException(nameof(project));

			var projectFile = this.FileSystem.FileInfo.FromFileName(project.FullName);

			var oneDirectoryUpParent = projectFile.Directory.Parent.Parent.FullName;

			var oneDirectoryUp = this.FileSystem.Path.Combine(oneDirectoryUpParent, projectFile.Directory.Name);

			return this.MoveProject(project, oneDirectoryUp);
		}

		protected internal virtual void RemoveDestinationDirectory()
		{
			var destinationDirectoryPath = this.DestinationDirectoryPath;

			if(!string.IsNullOrEmpty(destinationDirectoryPath) && this.FileSystem.Directory.Exists(destinationDirectoryPath))
				this.FileSystem.Directory.Delete(destinationDirectoryPath, true);
		}

		protected internal virtual void Rollback()
		{
			this.RemoveDestinationDirectory();

			if(!this.IsNewSolution)
				return;

			var solutionDirectoryPath = this.SolutionDirectoryPath;

			if(!string.IsNullOrEmpty(solutionDirectoryPath) && this.FileSystem.Directory.Exists(solutionDirectoryPath))
			{
				var solutionDirectoryIsEmpty = !this.FileSystem.Directory.GetFiles(solutionDirectoryPath).Concat(this.FileSystem.Directory.GetDirectories(solutionDirectoryPath)).Any();

				if(solutionDirectoryIsEmpty)
					this.FileSystem.Directory.Delete(solutionDirectoryPath, true);
			}
		}

		protected internal virtual bool ShowProjectPropertiesForm()
		{
			using(var projectPropertiesForm = this.CreateProjectPropertiesForm())
			{
				projectPropertiesForm.AssemblyName = this.AssemblyName;
				projectPropertiesForm.RootNamespace = this.RootNamespace;
				projectPropertiesForm.Heading = this.WizardHeading + " - " + this.ProjectName;

				return this.ShowProjectPropertiesFormDialog(projectPropertiesForm);
			}
		}

		protected internal virtual bool ShowProjectPropertiesFormDialog(IProjectPropertiesForm projectPropertiesForm)
		{
			if(projectPropertiesForm == null)
				throw new ArgumentNullException(nameof(projectPropertiesForm));

			var dialogResult = projectPropertiesForm.ShowDialog();

			if(dialogResult == DialogResult.OK)
			{
				if(string.IsNullOrWhiteSpace(projectPropertiesForm.AssemblyName) || string.IsNullOrWhiteSpace(projectPropertiesForm.RootNamespace))
				{
					this.MessageBoxFactory.Create().ShowError("You must enter a value for both \"Assembly-name\" and \"Root-namespace\".");

					return this.ShowProjectPropertiesFormDialog(projectPropertiesForm);
				}

				this.AssemblyName = projectPropertiesForm.AssemblyName;
				this.RootNamespace = projectPropertiesForm.RootNamespace;

				return true;
			}

			return false;
		}

		#endregion
	}
}