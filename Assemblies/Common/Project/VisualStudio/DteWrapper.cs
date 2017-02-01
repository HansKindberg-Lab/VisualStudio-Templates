using System;
using System.Diagnostics.CodeAnalysis;
using EnvDTE;
using HansKindberg.Abstractions;

namespace HansKindberg.VisualStudio
{
	[CLSCompliant(false)]
	public abstract class DteWrapper<T> : Wrapper<T>, IDevelopmentToolsEnvironment where T : DTE
	{
		#region Constructors

		protected DteWrapper(T dte, string parameterName) : base(dte, parameterName) {}

		#endregion

		#region Properties

		public virtual string Edition => this.WrappedInstance.Edition;

		#endregion
	}

	[CLSCompliant(false)]
	public class DteWrapper : DteWrapper<DTE>
	{
		#region Constructors

		public DteWrapper(DTE dte) : base(dte, nameof(dte)) {}

		#endregion

		#region Methods

		public static DteWrapper FromDte(DTE dte)
		{
			return dte != null ? new DteWrapper(dte) : null;
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static DTE ToDte(DteWrapper dteWrapper)
		{
			return dteWrapper?.WrappedInstance;
		}

		#endregion
	}
}