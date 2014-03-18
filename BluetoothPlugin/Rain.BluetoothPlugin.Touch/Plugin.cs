using System;
using Rain.BluetoothPlugin;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Rain.BluetoothPlugin.Touch
{
	public class Plugin
		: IMvxPlugin
	{
		public void Load()
		{
			Mvx.RegisterSingleton<IBluetoothManager>(new MvxBluetoothManager());
		}
	}
}

