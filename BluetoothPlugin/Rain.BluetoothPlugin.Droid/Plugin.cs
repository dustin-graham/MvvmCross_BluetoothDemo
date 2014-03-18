using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Rain.BluetoothPlugin.Droid
{
	public class Plugin : IMvxPlugin
	{

		#region IMvxPlugin implementation

		public void Load ()
		{
			Mvx.RegisterSingleton<IBluetoothManager> (new MvxBluetoothManager ());
		}

		#endregion
	}
}

