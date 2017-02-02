using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace HansKindberg.VisualStudio.Templating.Wizards
{
	[AttributeUsage(AttributeTargets.Class)]
	[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
	public class WizardControllerAttribute : Attribute
	{
		#region Fields

		private static readonly Type _wizardControllerInterfaceType = typeof(IWizardController);

		#endregion

		#region Constructors

		public WizardControllerAttribute(Type wizardControllerType)
		{
			if(wizardControllerType == null)
				throw new ArgumentNullException(nameof(wizardControllerType));

			if(!_wizardControllerInterfaceType.IsAssignableFrom(wizardControllerType))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The wizard-controller-type \"{0}\" must inherit from \"{1}\".", wizardControllerType, _wizardControllerInterfaceType), nameof(wizardControllerType));

			this.WizardControllerType = wizardControllerType;
		}

		#endregion

		#region Properties

		public virtual Type WizardControllerType { get; }

		#endregion
	}
}