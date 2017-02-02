using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public abstract class BasicWizardController : IWizardController
	{
		#region Constructors

		protected BasicWizardController(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard)
		{
			if(developmentToolsEnvironment == null)
				throw new ArgumentNullException(nameof(developmentToolsEnvironment));

			if(parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			if(replacements == null)
				throw new ArgumentNullException(nameof(replacements));

			if(wizard == null)
				throw new ArgumentNullException(nameof(wizard));

			this.DevelopmentToolsEnvironment = developmentToolsEnvironment;
			this.Parameters = parameters;
			this.Replacements = replacements;
			this.RunKind = runKind;
			this.Wizard = wizard;
		}

		#endregion

		#region Properties

		public virtual IDevelopmentToolsEnvironment DevelopmentToolsEnvironment { get; }
		public virtual IEnumerable<object> Parameters { get; }
		public virtual IDictionary<string, string> Replacements { get; }
		public virtual WizardRunKind RunKind { get; }
		public virtual IWizard Wizard { get; }

		#endregion
	}
}