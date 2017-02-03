namespace HansKindberg
{
	public interface IBootstrapper
	{
		#region Properties

		bool Initialized { get; }

		#endregion

		#region Methods

		void Initialize();

		#endregion
	}
}