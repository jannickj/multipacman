using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace iilang
{
	class MainClass
	{
		public static void Main (string[] args)
		{

//			Percept f = new Percept (
//				"navn", 
//				new Identifier ("hej"),
//				new ParameterList(
//					new Numeral(3),
//			        new Identifier("hello")
//			    )
//			);
//
//			XmlSerializer ser = new XmlSerializer(typeof(Percept));
//			XDocument e = new XDocument ();
//			XmlWriter w = e.CreateWriter ();
//			ser.Serialize (w, f);
//
//			Console.WriteLine (e);

			
			XmlSerializer ser = new XmlSerializer (typeof(EisPercept));
			XDocument xwriter = new XDocument ();
			XDocument xreader = XDocument.Parse(
				@"<percept name=""hej"">
					<perceptParameter>
						<identifier value=""hejhej"" />
					</perceptParameter>
					<perceptParameter>
						<function name=""fun"">
							<number value=""42"" />
						</function>
					</perceptParameter>
				</percept>");
			XmlReader r = xreader.CreateReader ();
			XmlWriter w = xwriter.CreateWriter ();

			var par = ser.Deserialize (r);
			ser.Serialize (w, par);
			
			Console.WriteLine (xwriter);


		}
	}
}

