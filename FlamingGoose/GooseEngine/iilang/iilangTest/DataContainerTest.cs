using System;
using NUnit.Framework;
using System.Xml.Linq;
using iilang;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace iilangTest
{
	[TestFixture()]
	public class DataContainerTest
	{
		[Test()]
		public void ActionFromXml_XmlRepresentationOfAction_ActionObject ()
		{
			EisAction expected = new EisAction ("moveTo", new EisNumeral (2), new EisNumeral (3));

			XmlSerializer serializer = new XmlSerializer (typeof(EisAction));

			XElement actual_src = XElement.Parse (
				@"<action name=""moveTo"">
					<actionParameter>
						<number value=""2.0"" />
					</actionParameter>
					<actionParameter>
						<number value=""3.0"" />
					</actionParameter>
				</action>");

			EisAction actual = (EisAction)serializer.Deserialize (actual_src.CreateReader ());
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void PerceptToXml_PerceptObject_XmlRepresentationOfPercept ()
		{
			EisPercept actual_src = new EisPercept (
				"sensors",
				new EisParameterList (
					new EisFunction ("red", new EisIdentifier ("ball")),
					new EisFunction ("rubber", new EisIdentifier ("ball"))
				)
			);

			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisPercept));
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

