﻿using System;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Rain.BluetoothPlugin
{
	public class PluginLoader
		: IMvxPluginLoader
	{
		public static readonly PluginLoader Instance = new PluginLoader();

		public void EnsureLoaded()
		{
			var manager = Mvx.Resolve<IMvxPluginManager>();
			manager.EnsurePlatformAdaptionLoaded<PluginLoader>();
		}
	}
}
