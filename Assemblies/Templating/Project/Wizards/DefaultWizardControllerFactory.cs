using System;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class DefaultWizardControllerFactory : IWizardControllerFactory
	{
		#region Methods

		public virtual IWizardController Create(object automationInstance, IDictionary<string, string> replacements, WizardRunKind runKind, object[] parameters)
		{
			return new WizardController(DteWrapper.FromDte((DTE) automationInstance), parameters, replacements, runKind);
		}

		#endregion
	}
}