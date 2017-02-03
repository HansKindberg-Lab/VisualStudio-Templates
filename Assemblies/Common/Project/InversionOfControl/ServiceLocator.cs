namespace HansKindberg.InversionOfControl
{
	public static class ServiceLocator
	{
		#region Fields

		private static volatile IServiceLocator _instance;
		private static readonly object _instanceLock = new object();

		#endregion

		#region Properties

		public static IServiceLocator Instance
		{
			get
			{
				// ReSharper disable InvertIf
				if(_instance == null)
				{
					lock(_instanceLock)
					{
						if(_instance == null)
							_instance = new DefaultServiceLocator();
					}
				}
				// ReSharper restore InvertIf

				return _instance;
			}
			set
			{
				if(value == _instance)
					return;

				lock(_instanceLock)
				{
					_instance = value;
				}
			}
		}

		#endregion
	}
}