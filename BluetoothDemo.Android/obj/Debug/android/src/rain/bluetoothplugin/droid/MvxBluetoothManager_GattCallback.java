package rain.bluetoothplugin.droid;


public class MvxBluetoothManager_GattCallback
	extends android.bluetooth.BluetoothGattCallback
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onConnectionStateChange:(Landroid/bluetooth/BluetoothGatt;II)V:GetOnConnectionStateChange_Landroid_bluetooth_BluetoothGatt_IIHandler\n" +
			"n_onServicesDiscovered:(Landroid/bluetooth/BluetoothGatt;I)V:GetOnServicesDiscovered_Landroid_bluetooth_BluetoothGatt_IHandler\n" +
			"";
		mono.android.Runtime.register ("Rain.BluetoothPlugin.Droid.MvxBluetoothManager/GattCallback, Rain.BluetoothPlugin.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", MvxBluetoothManager_GattCallback.class, __md_methods);
	}


	public MvxBluetoothManager_GattCallback () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxBluetoothManager_GattCallback.class)
			mono.android.TypeManager.Activate ("Rain.BluetoothPlugin.Droid.MvxBluetoothManager/GattCallback, Rain.BluetoothPlugin.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MvxBluetoothManager_GattCallback (rain.bluetoothplugin.droid.MvxBluetoothManager p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxBluetoothManager_GattCallback.class)
			mono.android.TypeManager.Activate ("Rain.BluetoothPlugin.Droid.MvxBluetoothManager/GattCallback, Rain.BluetoothPlugin.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", "Rain.BluetoothPlugin.Droid.MvxBluetoothManager, Rain.BluetoothPlugin.Droid, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onConnectionStateChange (android.bluetooth.BluetoothGatt p0, int p1, int p2)
	{
		n_onConnectionStateChange (p0, p1, p2);
	}

	private native void n_onConnectionStateChange (android.bluetooth.BluetoothGatt p0, int p1, int p2);


	public void onServicesDiscovered (android.bluetooth.BluetoothGatt p0, int p1)
	{
		n_onServicesDiscovered (p0, p1);
	}

	private native void n_onServicesDiscovered (android.bluetooth.BluetoothGatt p0, int p1);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
