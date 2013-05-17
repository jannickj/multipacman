using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JSLibrary
{
	public class ExtendedString
	{
		public static Stream ToStream(string str)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(str);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}
