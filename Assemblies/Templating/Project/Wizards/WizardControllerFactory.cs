using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using StructureMap;
using StructureMap.Pipeline;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class WizardControllerFactory : BasicWizardControllerFactory
	{
		#region Constructors

		public WizardControllerFactory(IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException(nameof(container));

			this.Container = container;
		}

		#endregion

		#region Properties

		protected internal virtual IContainer Container { get; }

		#endregion

		#region Methods

		public override IWizardController Create(IDevelopmentToolsEnvironment developmentToolsEnvironment, IEnumerable<object> parameters, IDictionary<string, string> replacements, WizardRunKind runKind, IWizard wizard, Type wizardControllerType)
		{
			if(developmentToolsEnvironment == null)
				throw new ArgumentNullException(nameof(developmentToolsEnvironment));

			if(parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			if(replacements == null)
				throw new ArgumentNullException(nameof(replacements));

			if(wizard == null)
				throw new ArgumentNullException(nameof(wizard));

			if(wizardControllerType == null)
				throw new ArgumentNullException(nameof(wizardControllerType));

			return (IWizardController) this.Container.GetInstance(wizardControllerType, new ExplicitArguments(new Dictionary<string, object>
			{
				{"developmentToolsEnvironment", developmentToolsEnvironment},
				{"parameters", parameters},
				{"replacements", replacements},
				{"runKind", runKind},
				{"wizard", wizard}
			}));
		}

		#endregion
	}
}