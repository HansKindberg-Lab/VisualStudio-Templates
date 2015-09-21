using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyDescription("Shim-tests for $ext_assembly-name$.")]
[assembly: CLSCompliant(true)]
[assembly: Guid("$guid1$")]

// ReSharper disable CheckNamespace
internal static class AssemblyInfo // ReSharper restore CheckNamespace
{
	#region Fields

	internal const string AssemblyName = "$ext_assembly-name$$test-suffix$";

	#endregion
}