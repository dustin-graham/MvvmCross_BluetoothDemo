using Cirrious.MvvmCross.ViewModels;
using Rain.BluetoothPlugin;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.CrossCore;

namespace BluetoothDemo.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
		private readonly IBluetoothManager _btManager;
		public FirstViewModel (IBluetoothManager btManager)
		{
			_btManager = btManager;
			_btManager.OnDeviceDiscovered += OnDeviceDiscovered;
			_btManager.OnScanTimeoutElapsed += HandleOnScanTimeoutElapsed;
		}


		private ObservableCollection<BluetoothDevice> _devices = new ObservableCollection<BluetoothDevice>();
		public ObservableCollection<BluetoothDevice> Devices {
			get {
				return _devices;
			}
			set {
				_devices = value;
				RaisePropertyChanged (() => Devices);
			}
		}

		private bool _isScanning = false;
		private IMvxCommand _scanCommand;
		public IMvxCommand ScanCommand {
			get {
				_scanCommand = _scanCommand ?? new MvxCommand (() => {
					if (_isScanning) return;
					_isScanning = true;
					DoScan();
				});
				return _scanCommand;
			}
		}

		private void DoScan() {
			Devices.Clear();
			_btManager.StartScanForDevices ();
		}

		public ICommand ConnectCommand
		{
			get
			{
				return new MvxCommand<BluetoothDevice>(async item => {
//					_btManager.ConnectToDevice(item.DeviceAddress);
					BluetoothDevice connectedDevice = await _btManager.ConnectToDeviceAsync("1234");
					Mvx.Trace("connected to device asynchronously: {0}: {1}", connectedDevice.DeviceName, connectedDevice.DeviceAddress);
				});
			}
		}
			

		private void OnDeviceDiscovered(object sender, DeviceDiscoveredEventArgs e) {
			InvokeOnMainThread (() => {
				BluetoothDevice device = e.Device;
				Devices.Add (device);
			});
		}


		void HandleOnScanTimeoutElapsed (object sender, System.EventArgs e)
		{
			InvokeOnMainThread (() => {
				_isScanning = false;
			});
		}
    }
}
