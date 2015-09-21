using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using EnvDTE;

namespace HansKindberg.VisualStudio.Templates.Wizards.IntegrationTests.Helpers
{
	public static class EnvironmentHelper
	{
		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static DTE GetActiveDevelopmentToolsEnvironment()
		{
			//return (DTE)Marshal.GetActiveObject("VisualStudio.DTE.12.0");
			return (DTE) Marshal.GetActiveObject("VisualStudio.DTE");
		}

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static Solution GetActiveSolution()
		{
			return GetActiveDevelopmentToolsEnvironment().Solution;
		}

		#endregion
	}
}