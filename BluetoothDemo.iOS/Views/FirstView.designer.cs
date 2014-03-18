// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BluetoothDemo.iOS
{
	[Register ("FirstView")]
	partial class FirstView
	{
		[Outlet]
		MonoTouch.UIKit.UITableView DeviceTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DeviceTableView != null) {
				DeviceTableView.Dispose ();
				DeviceTableView = null;
			}
		}
	}
}
