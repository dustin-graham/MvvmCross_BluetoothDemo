using System;
using Rain.BluetoothPlugin;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Bluetooth;
using Android.Content;

using Cirrious.CrossCore;
using Cirrious.MvvmCross;
using Android.Content.PM;
using Cirrious.CrossCore.Platform;

namespace Rain.BluetoothPlugin.Droid
{
	public class MvxBluetoothManager : Java.Lang.Object, IBluetoothManager, BluetoothAdapter.ILeScanCallback
	{
		public event EventHandler OnScanTimeoutElapsed = delegate {};
		public event EventHandler<DeviceDiscoveredEventArgs> OnDeviceDiscovered;

//		public event EventHandler ScanTimeoutElapsed = delegate {};
//		public event EventHandler<DeviceConnectionEventArgs> DeviceConnected = delegate {};
//		public event EventHandler<DeviceConnectionEventArgs> DeviceDisconnected = delegate {};
//		public event EventHandler<ServiceDiscoveredEventArgs> ServiceDiscovered = delegate {};

		private BluetoothManager _btManager;
		private BluetoothAdapter _btAdapter;

		static readonly int SCAN_PERIOD = 10000;

		private bool _btLeSupported = true;

		private bool mScanning;
		private Context _appContext;

		private GattCallback _gattCallback;

		TaskCompletionSource<BluetoothDevice> mConnectionTaskSource;

		public MvxBluetoothManager ()
		{
			//how you might do the actual Bluetooth Work
			var globals = Mvx.Resolve<Cirrious.CrossCore.Droid.IMvxAndroidGlobals>();
			_appContext = globals.ApplicationContext;
			_btManager = (BluetoothManager)globals.ApplicationContext.GetSystemService (Context.BluetoothService);
			_btAdapter = _btManager.Adapter;

			if (_btAdapter == null || !globals.ApplicationContext.PackageManager.HasSystemFeature (Android.Content.PM.PackageManager.FeatureBluetoothLe)) {
				_btLeSupported = false;
				MvxTrace.Trace (() => "BluetoothLE not supported on this device");
			}

			_gattCallback = new GattCallback (this);
		}

		private Dictionary<string, Android.Bluetooth.BluetoothDevice> _deviceMap = new Dictionary<string, Android.Bluetooth.BluetoothDevice>();
		private Dictionary<string, Android.Bluetooth.BluetoothGatt> _connectedDevices = new Dictionary<string, Android.Bluetooth.BluetoothGatt> ();

		#region IBluetoothAdapter implementation

		public Task<BluetoothDevice> ConnectToDeviceAsync (string deviceAddress)
		{
			mConnectionTaskSource = new TaskCompletionSource<BluetoothDevice> ();
			DoSomethingThatTakesAWhile (5000, mConnectionTaskSource);
			return mConnectionTaskSource.Task;
		}

		private async void DoSomethingThatTakesAWhile(int duration, TaskCompletionSource<BluetoothDevice> completionSource) {
			await Task.Delay (duration);
			completionSource.SetResult (new BluetoothDevice () {
				DeviceName = "Async Device",
				DeviceAddress = "123456"
			});
		}

		public async void StartScanForDevices ()
		{
			// clear out the list
			_deviceMap.Clear ();

			// start scanning
			mScanning = true;
			_btAdapter.StartLeScan(this);

			await Task.Delay (SCAN_PERIOD);

			//if we're still scanning
			if (mScanning) {
				Console.WriteLine ("BluetoothLEManager: Scan timeout has elapsed.");
				_btAdapter.StopLeScan (this);

				OnScanTimeoutElapsed (this, new EventArgs ());
			}
		}

		public void ConnectToDevice (string deviceAddress)
		{
			Android.Bluetooth.BluetoothDevice androidDevice = _deviceMap [deviceAddress];
			if (androidDevice != null) {
				ConnectToDevice (androidDevice);
			}
		}

		public void ConnectToDevice (BluetoothDevice device)
		{
			Android.Bluetooth.BluetoothDevice androidDevice = _deviceMap [device.DeviceAddress];
			if (androidDevice != null) {
				ConnectToDevice (androidDevice);
			}
		}

		private void ConnectToDevice (Android.Bluetooth.BluetoothDevice device) {
			mConnectionTaskSource = new TaskCompletionSource<BluetoothDevice> ();
			device.ConnectGatt (_appContext, false, _gattCallback);

		}

		#endregion

		#region ILeScanCallback implementation

		public void OnLeScan (Android.Bluetooth.BluetoothDevice device, int rssi, byte[] scanRecord)
		{
			if (!_deviceMap.ContainsKey(device.Address)) {
				BluetoothDevice d = new BluetoothDevice () {
					DeviceName = device.Name,
					DeviceAddress = device.Address
				};
				_deviceMap.Add (device.Address, device);
				OnDeviceDiscovered (this, new DeviceDiscoveredEventArgs () {
					Device = d
				});
			}
		}

		#endregion

		public class DeviceConnectionEventArgs : EventArgs
		{
			public Android.Bluetooth.BluetoothDevice Device;

			public DeviceConnectionEventArgs() : base()
			{}
		}

		public class ServiceDiscoveredEventArgs : EventArgs
		{
			public BluetoothGatt Gatt;

			public ServiceDiscoveredEventArgs() : base ()
			{}
		}

		protected class GattCallback : BluetoothGattCallback
		{
			protected MvxBluetoothManager _parent;
			
			public GattCallback (MvxBluetoothManager parent)
			{
				this._parent = parent;
			}
			
			public override void OnConnectionStateChange (BluetoothGatt gatt, GattStatus status, ProfileState newState)
			{
				Console.WriteLine ("OnConnectionStateChange: ");
				base.OnConnectionStateChange (gatt, status, newState);
				
				switch (newState) {
				// disconnected
				case ProfileState.Disconnected:
					Console.WriteLine ("disconnected");
					//TODO/BUG: Need to remove this, but can't remove the key (uncomment and see bug on disconnect)
					//					if (this._parent._connectedDevices.ContainsKey (gatt.Device))
					//						this._parent._connectedDevices.Remove (gatt.Device);
//					this._parent.DeviceDisconnected (this, new DeviceConnectionEventArgs () { Device = gatt.Device });
					break;
					// connecting
				case ProfileState.Connecting:
					Console.WriteLine ("Connecting");
					break;
					// connected
				case ProfileState.Connected:
					Console.WriteLine ("Connected");
					//TODO/BUGBUG: need to remove this when disconnected
//					this._parent._connectedDevices.Add (gatt.Device, gatt);
//					this._parent.DeviceConnected (this, new DeviceConnectionEventArgs () { Device = gatt.Device });
					this._parent._connectedDevices.Add (gatt.Device.Address, gatt);
					break;
					// disconnecting
				case ProfileState.Disconnecting:
					Console.WriteLine ("Disconnecting");
					break;
				}
			}
			
			public override void OnServicesDiscovered (BluetoothGatt gatt, GattStatus status)
			{
				base.OnServicesDiscovered (gatt, status);
				
				Console.WriteLine ("OnServicesDiscovered: " + status.ToString ());
				
				//TODO: somehow, we need to tie this directly to the device, rather than for all
				// google's API deisgners are children.
				
				//TODO: not sure if this gets called after all services have been enumerated or not
//				if(!this._parent._services.ContainsKey(gatt.Device))
//					this._parent.Services.Add(gatt.Device, this._parent._connectedDevices [gatt.Device].Services);
//				else
//					this._parent._services[gatt.Device] = this._parent._connectedDevices [gatt.Device].Services;
//				
//				this._parent.ServiceDiscovered (this, new ServiceDiscoveredEventArgs () {
//					Gatt = gatt
//				});
			}
			
		}
	}

}

