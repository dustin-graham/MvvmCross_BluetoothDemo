using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rain.BluetoothPlugin
{
	public interface IBluetoothManager
	{
		event EventHandler OnScanTimeoutElapsed;
		event EventHandler<DeviceDiscoveredEventArgs> OnDeviceDiscovered;
		void StartScanForDevices();
		void ConnectToDevice (string deviceAddress);
		void ConnectToDevice(BluetoothDevice device);
		Task<BluetoothDevice> ConnectToDeviceAsync (string deviceAddress);
	}

	public class DeviceDiscoveredEventArgs : EventArgs
	{
		public BluetoothDevice Device;

		public DeviceDiscoveredEventArgs() : base()
		{}
	}
}

