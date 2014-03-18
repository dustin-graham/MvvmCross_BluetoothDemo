using System;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using BluetoothDemo.Core;
using BluetoothDemo.Core.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;

namespace BluetoothDemo.iOS
{
	public partial class FirstView : MvxViewController
	{
		public FirstView () : base ("FirstView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Bluetooth Scan";

			// ios7 layout
			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			NavigationController.NavigationBar.Translucent = false;

			var navButton = new UIBarButtonItem ("Scan", UIBarButtonItemStyle.Plain, DoScan);
			NavigationItem.RightBarButtonItem = navButton;

			var source = new MvxStandardTableViewSource(DeviceTableView, "TitleText DeviceName");
			DeviceTableView.Source = source;

			// Perform any additional setup after loading the view, typically from a nib.
			var set = this.CreateBindingSet<FirstView, FirstViewModel> ();
			set.Bind (source).To (vm => vm.Devices);
			set.Apply ();
		}

		private void DoScan(object caller, System.EventArgs args) {
			((FirstViewModel)this.ViewModel).ScanCommand.Execute ();
		}
	}
}

