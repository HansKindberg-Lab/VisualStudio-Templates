using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using EnvDTE;
using HansKindberg.VisualStudio.Templating.Old.Environment.Extensions;
using HansKindberg.VisualStudio.Templating.Old.Forms;
using Microsoft.VisualStudio.TemplateWizard;
using SystemEnvironment = System.Environment;

namespace HansKindberg.VisualStudio.Templating.Old
{
	[CLSCompliant(false)]
	public abstract class Wizard : IWizard
	{
		#region Fields

		private const string _additionalInformation = "Press Ctrl-C to copy this message to the clipboard (the message-box must have focus for it to work).";
		private IEnumerable<object> _customParameters;
		private const string _destinationDirectoryKey = "$destinationdirectory$";
		private DTE _developmentToolsEnvironment;
		private const string _projectNameKey = "$projectname$";
		private IDictionary<string, string> _replacements;
		private const string _safeProjectNameKey = "$safeprojectname$";
		private const string _solutionDirectoryKey = "$solutiondirectory$";
		private WizardRunKind _wizardRunKind;

		#endregion

		#region Constructors

		protected Wizard(IFileSystem fileSystem, IMessageBoxFactory messageBoxFactory)
		{
			if(fileSystem == null)
				throw new ArgumentNullException(nameof(fileSystem));

			if(messageBoxFactory == null)
				throw new ArgumentNullException(nameof(messageBoxFactory));

			this.FileSystem = fileSystem;
			this.MessageBoxFactory = messageBoxFactory;
		}

		#endregion

		#region Properties

		protected internal virtual string AdditionalInformation
		{
			get { return SystemEnvironment.NewLine + SystemEnvironment.NewLine + _additionalInformation; }
		}

		protected internal virtual IEnumerable<object> CustomParameters
		{
			get { return this._customParameters; }
		}

		protected internal virtual DirectoryInfoBase DestinationDirectory
		{
			get { return this.FileSystem.DirectoryInfo.FromDirectoryName(this.DestinationDirectoryPath); }
		}

		protected internal virtual string DestinationDirectoryPath
		{
			get { return this.GetReplacementValue(_destinationDirectoryKey); }
			set { this.SetReplacementValue(_destinationDirectoryKey, value); }
		}

		protected internal virtual DTE DevelopmentToolsEnvironment
		{
			get { return this._developmentToolsEnvironment; }
		}

		protected internal virtual IFileSystem FileSystem { get; }

		protected internal virtual bool IsNewSolution
		{
			get { return string.IsNullOrEmpty(this.Solution.FullName); }
		}

		protected internal virtual IMessageBoxFactory MessageBoxFactory { get; }

		protected internal virtual string ProjectName
		{
			get { return this.GetReplacementValue(_projectNameKey); }
			set { this.SetReplacementValue(_projectNameKey, value); }
		}

		protected internal virtual IDictionary<string, string> Replacements
		{
			get { return this._replacements; }
		}

		protected internal virtual string SafeProjectName
		{
			get { return this.GetReplacementValue(_safeProjectNameKey); }
			set { this.SetReplacementValue(_safeProjectNameKey, value); }
		}

		protected internal virtual Solution Solution
		{
			get { return this.DevelopmentToolsEnvironment.Solution; }
		}

		protected internal virtual string SolutionDirectoryPath
		{
			get { return this.GetReplacementValue(_solutionDirectoryKey); }
		}

		protected internal virtual WizardRunKind WizardRunKind
		{
			get { return this._wizardRunKind; }
		}

		#endregion

		#region Methods

		protected internal virtual string AddInformationToMessage(string message)
		{
			return message + this.AdditionalInformation;
		}

		public virtual void BeforeOpeningFile(ProjectItem projectItem) {}

		protected internal virtual void Cancel()
		{
			throw new WizardBackoutException();
		}

		protected internal virtual Project GetProject(string projectName)
		{
			if(string.IsNullOrEmpty(projectName))
				return null;

			var potentialProjects = this.Solution.GetAllProjects().Where(project => string.Equals(project.Name, projectName)).ToArray();

			if(potentialProjects.Count() > 1)
				return potentialProjects.FirstOrDefault(project => project.FullName.StartsWith(this.DestinationDirectoryPath, StringComparison.OrdinalIgnoreCase));

			return potentialProjects.FirstOrDefault();
		}

		protected internal virtual string GetReplacementValue(string key)
		{
			return this.Replacements.ContainsKey(key) ? this.Replacements[key] : null;
		}

		protected internal virtual void HandleException(string message)
		{
			this.MessageBoxFactory.Create().ShowError(this.AddInformationToMessage(message));
		}

		protected internal virtual void HandleException(Exception exception)
		{
			var message = string.Empty;

			if(exception != null)
			{
				message += exception.GetType().FullName + ": " + exception.Message + SystemEnvironment.NewLine + SystemEnvironment.NewLine;

				message += "Full message with stack-trace: " + exception;
			}

			this.HandleException(message);
		}

		public virtual void ProjectFinishedGenerating(Project project) {}
		public virtual void ProjectItemFinishedGenerating(ProjectItem projectItem) {}
		protected internal abstract void Run();
		public virtual void RunFinished() {}

		public virtual void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
		{
			try
			{
				this._customParameters = customParams;
				this._developmentToolsEnvironment = (DTE) automationObject;
				this._replacements = replacementsDictionary;
				this._wizardRunKind = runKind;
			}
			catch(Exception exception)
			{
				this.HandleException(exception);

				this.Cancel();
			}

			this.Run();
		}

		protected internal virtual void SetReplacementValue(string key, string value)
		{
			if(value == null)
			{
				this.Replacements.Remove(key);
				return;
			}

			this.Replacements[key] = value;
		}

		public virtual bool ShouldAddProjectItem(string filePath)
		{
			return true;
		}

		#endregion
	}
}