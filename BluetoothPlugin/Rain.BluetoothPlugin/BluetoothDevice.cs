using System;

namespace Rain.BluetoothPlugin
{
	public class BluetoothDevice
	{
		public BluetoothDevice ()
		{
		}

		public string DeviceAddress {
			get;
			set;
		}

		public string DeviceName {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("[BluetoothDevice: DeviceAddress={0}, DeviceName={1}]", DeviceAddress, DeviceName);
		}
	}
}

