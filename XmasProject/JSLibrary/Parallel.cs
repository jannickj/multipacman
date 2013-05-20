using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JSLibrary
{
	public class Parallel
	{
		public static T Execute<T>(Func<T> func, int timeout)
		{
			T result;
			TryExecute(func, timeout, out result);
			return result;
		}

		public static bool TryExecute<T>(Func<T> func, int timeout, out T result)
		{
			T t = default(T);
			Thread thread = new Thread(() => t = func());
			thread.Start();
			bool completed = thread.Join(timeout);
			if (!completed)
			{
				thread.Abort();
				throw new TimeoutException();
			}
			result = t;
			return completed;
		}
	}
}
