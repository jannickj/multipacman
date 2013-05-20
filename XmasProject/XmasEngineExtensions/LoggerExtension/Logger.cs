using System;
using System.IO;

namespace XmasEngineExtensions.LoggerExtension
{
	public class Logger
	{
		private StreamWriter logstream;
		private DebugLevel debugLevel;

		public DebugLevel DebugLevel {
			get { return debugLevel; }
			set { debugLevel = value; }
		}

		public Logger (StreamWriter logstream, DebugLevel debugLevel)
		{
			this.logstream = logstream;
			this.debugLevel = debugLevel;
		}

		public void LogStringWithTimeStamp (string str, DebugLevel debugLevel)
		{
			if (debugLevel <= this.DebugLevel) {
				logstream.WriteLine ("{0} : {1}", TimeStamp(), str);
				logstream.Flush ();
			}
		}
		
		internal string TimeStamp()
		{
			return DateTime.Now.ToString ("[dd/MM-yyyy HH:mm:ss]");
		}
	}
}

