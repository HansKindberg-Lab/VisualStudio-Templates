using System;
using System.Globalization;
using HansKindberg.InversionOfControl;
using HansKindberg.VisualStudio.Templating.Wizards;

namespace HansKindberg.VisualStudio.Templating.InversionOfControl
{
	[CLSCompliant(false)]
	public class StaticServiceLocator : DefaultServiceLocator
	{
		#region Properties

		protected internal virtual IWizardControllerFactory WizardControllerFactory { get; } = new DefaultWizardControllerFactory();

		#endregion

		#region Methods

		public override object GetService(Type serviceType)
		{
			if(serviceType == null)
				throw new ArgumentNullException(nameof(serviceType));

			if(serviceType == typeof(IWizardControllerFactory))
				return this.WizardControllerFactory;

			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Can not get an instance of type \"{0}\".", serviceType));
		}

		#endregion
	}
}