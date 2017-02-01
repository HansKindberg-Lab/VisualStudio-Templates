using System;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	public class Wizard : BasicWizard
	{
		#region Constructors

		public Wizard() : base(Wizards.WizardControllerFactory.Instance) {}

		#endregion
	}
}