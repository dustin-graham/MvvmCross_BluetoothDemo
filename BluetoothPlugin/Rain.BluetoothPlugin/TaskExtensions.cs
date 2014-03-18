using System;
using System.Threading.Tasks;

namespace Rain.BluetoothPlugin
{
	public static class TaskExtensions
	{
		public static async Task TimeoutAfter(this Task task, int millisecondsTimeout)
		{
			if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout))) 
				await task;
			else
				throw new TimeoutException();
		}
	}
}

