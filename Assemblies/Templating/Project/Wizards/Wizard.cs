using System;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	[WizardController(typeof(WizardController))]
	public class Wizard : BasicWizard
	{
		#region Fields

		private static readonly IWizardHost _wizardHost = new WizardHost(new WizardControllerFactory());

		#endregion

		#region Constructors

		public Wizard() : base(_wizardHost) {}

		#endregion
	}
}