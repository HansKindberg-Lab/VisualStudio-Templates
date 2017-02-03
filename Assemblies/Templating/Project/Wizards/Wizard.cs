using System;
using System.Windows.Forms;
using HansKindberg.InversionOfControl;
using Microsoft.VisualStudio.TemplateWizard;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[CLSCompliant(false)]
	[WizardController(typeof(WizardController))]
	public class Wizard : BasicWizard
	{
		#region Constructors

		public Wizard() : base(InitializeIfNecessaryAndGetWizardHost()) {}

		#endregion

		#region Methods

		private static IWizardHost InitializeIfNecessaryAndGetWizardHost()
		{
			try
			{
				if(!Bootstrapper.Instance.Initialized)
					Bootstrapper.Instance.Initialize();

				return ServiceLocator.Instance.GetService<IWizardHost>();
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);

				throw new WizardCancelledException();
			}
		}

		#endregion
	}
}