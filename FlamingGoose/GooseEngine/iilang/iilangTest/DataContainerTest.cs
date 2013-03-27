using System;
using NUnit.Framework;
using System.Xml.Linq;
using iilang;
using System.Xml.Serialization;

namespace iilangTest
{
	[TestFixture()]
	public class DataContainerTest
	{
		[Test()]
		public void ActionFromXml ()
		{

		}

		[Test()]
		public void PerceptToXml ()
		{
			Percept actual_src = new Percept (
				"sensors",
				new ParameterList (
					new Function ("red", new Identifier ("ball")),
					new Function ("rubber", new Identifier ("ball"))
				)
			);

			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(Percept));
			serializer.Serialize (actual.CreateWriter (), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<percept name=""sensors"">
					<perceptParameter>
						<parameterList>
							<function name=""red"">
								<identifier value=""ball"" />
							</function>
							<function name=""rubber"">
								<identifier value=""ball"" />
							</function>
						</parameterList>
					</perceptParameter>
				</percept>");
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}
	}
}

