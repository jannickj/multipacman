using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using GooseEngine.Data;

namespace iilang
{
	class MainClass
	{
		public static void Main (string[] args)
		{	
//			XmlSerializer ser = new XmlSerializer (typeof(EisPercept));
//			XDocument xwriter = new XDocument ();
//			XDocument xreader = XDocument.Parse(
//				@"<percept name=""hej"">
//					<perceptParameter>
//						<identifier value=""hejhej"" />
//					</perceptParameter>
//					<perceptParameter>
//						<function name=""fun"">
//							<number value=""42"" />
//						</function>
//					</perceptParameter>
//				</percept>");
//			XmlReader r = xreader.CreateReader ();
//			XmlWriter w = xwriter.CreateWriter ();
//
//			var par = ser.Deserialize (r);
//			ser.Serialize (w, par);
//			
//			Console.WriteLine (xwriter);
//
			Point test = new Vector(1,1) + new Point ();

		}
	}
}

