using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rain.BluetoothPlugin.Touch
{
	public class MvxBluetoothManager : IBluetoothManager
	{
		public event EventHandler OnScanTimeoutElapsed;

		public MvxBluetoothManager ()
		{
		}

		private List<BluetoothDevice> _bluetoothDevices;
		public List<BluetoothDevice> BluetoothDevices {
			get {
				if (_bluetoothDevices == null) {
					_bluetoothDevices = new List<BluetoothDevice> ();
					for (int i = 0; i < 5; i++) {
						var device = new BluetoothDevice () {
							DeviceAddress = "adf+" + i,
							DeviceName = "iOS Device " + i
						};
						_bluetoothDevices.Add (device);
					}
				}
				return _bluetoothDevices;
			}
			private set {
				_bluetoothDevices = value;
			}
		}

		#region IBluetoothAdapter implementation

		public Task<BluetoothDevice> ConnectToDeviceAsync (string deviceAddress)
		{
			throw new NotImplementedException ();
		}

		public event EventHandler<DeviceDiscoveredEventArgs> OnDeviceDiscovered;

		public async void StartScanForDevices ()
		{
			await Task.Delay (500);
		}

		public void ConnectToDevice (string deviceAddress)
		{

		}

		public void ConnectToDevice (BluetoothDevice device)
		{
//			await Task.Delay(200);
		}

		#endregion
	}
}

