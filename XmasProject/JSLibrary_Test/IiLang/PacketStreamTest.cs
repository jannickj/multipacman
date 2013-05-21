using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JSLibrary.Network;
using NUnit.Framework;

namespace JSLibrary_Test.IiLang
{
	[TestFixture]
	public class PacketStreamTest
	{

		[Test]
		public void ReadPacket_HelloWorldMsg_GetsPacketCorretly()
		{
			string expected = "Hello world";
			MemoryStream mems = new MemoryStream();
			PacketStream pstream = new PacketStream(mems);
			byte[] teststring = Encoding.UTF8.GetBytes(expected);
			byte[] len = BitConverter.GetBytes(teststring.Length);
			mems.Write(len, 0, len.Length);
			mems.Write(teststring,0, teststring.Length);

			pstream.ReadNextPackage();

			string actual = new StreamReader(pstream,Encoding.UTF8).ReadToEnd();
			
			Assert.AreEqual(expected,actual);
		}

		[Test]
		public void ReadAndSendPacket_ThreePackages_GetsTheSentPacketsCorrectly()
		{
			MemoryStream memsout = new MemoryStream();
			
			StreamWriter sw = new StreamWriter(new PacketStream(memsout));

			StreamReader sr = new StreamReader(new PacketStream(memsout));

			sw.WriteLine("packet 1");
			sw.Flush();
			sw.WriteLine("packet 2");
			sw.Flush();
			sw.WriteLine("packet 3");
			sw.Flush();

			string p1 = sr.ReadLine();
			string p2 = sr.ReadLine();
			string p3 = sr.ReadLine();

		}
	}
}
