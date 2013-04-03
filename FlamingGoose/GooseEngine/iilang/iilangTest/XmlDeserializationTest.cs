using System;
using NUnit.Framework;
using iilang;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace iilangTest
{
	[TestFixture()]
	public class XmlDeserializationTest
	{
		[Test()]
		public void TryReadXmlOfIdentifierWithValue_EisIdentifierObject ()
		{
			EisIdentifier expected = new EisIdentifier ("test_id");
			
			XmlSerializer serializer = new XmlSerializer (typeof(EisIdentifier));
			
			XElement actual_src = XElement.Parse (@"<identifier value=""test_id"" />");
			
			EisIdentifier actual = (EisIdentifier)serializer.Deserialize (actual_src.CreateReader ());
			Assert.AreEqual (expected, actual);
		}

		public void TryReadXmlOfIdentifierWithoutValue_ThrowException ()
		{
			XmlSerializer serializer = new XmlSerializer (typeof(EisIdentifier));
			
			XElement actual_src = XElement.Parse (@"<identifier />");
			Assert.Throws<MissingXmlAttributeException> (() => serializer.Deserialize (actual_src.CreateReader ()));
		}

		[Test()]
		public void TryReadXmlOfNumeralWithValue_EisNumeralObject ()
		{
			EisNumeral expected = new EisNumeral (42);
			
			XmlSerializer serializer = new XmlSerializer (typeof(EisNumeral));
			
			XElement actual_src = XElement.Parse (@"<number value=""42"" />");
			
			EisNumeral actual = (EisNumeral)serializer.Deserialize (actual_src.CreateReader ());
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void TryReadXmlOfNumeralWithoutValue_ThrowException ()
		{
			XmlSerializer serializer = new XmlSerializer (typeof(EisNumeral));
			
			XElement actual_src = XElement.Parse (@"<number />");
			Assert.Throws<MissingXmlAttributeException> (() => serializer.Deserialize (actual_src.CreateReader ()));
		}

		[Test()]
		public void ReadXmlOfFunctionWithChildren_EisFunctionObjectWithChildren ()
		{
			EisFunction expected = new EisFunction ("test_fun", new EisIdentifier ("test_id"), new EisNumeral (42));
			
			XmlSerializer serializer = new XmlSerializer (typeof(EisFunction));
			
			XElement actual_src = XElement.Parse (
				@"<function name=""test_fun"">
					<identifier value=""test_id"" />
					<number value=""42"" />
				</function>");
			
			EisFunction actual = (EisFunction)serializer.Deserialize (actual_src.CreateReader ());
			Assert.AreEqual (expected, actual);
		}

		[Test()]
		public void TryReadXmlOfFunctionWithoutName_ThrowException ()
		{	
			XmlSerializer serializer = new XmlSerializer (typeof(EisFunction));
			
			XElement actual_src = XElement.Parse (
				@"<function>
					<identifier value=""test_id"" />
					<number value=""42"" />
				</function>");

			Assert.Throws<MissingXmlAttributeException> (() => serializer.Deserialize (actual_src.CreateReader ()));
		}

		[Test()]
		public void ReadXmlRepresentationOfAction_ActionObject ()
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
		public void TryReadXmlRepresentationOfActionWithoutName_ActionObject ()
		{
			XmlSerializer serializer = new XmlSerializer (typeof(EisAction));
			
			XElement actual_src = XElement.Parse (
				@"<action>
					<actionParameter>
						<number value=""2.0"" />
					</actionParameter>
					<actionParameter>
						<number value=""3.0"" />
					</actionParameter>
				</action>");

			Assert.Throws<MissingXmlAttributeException> (() => serializer.Deserialize (actual_src.CreateReader ()));
		}
	}
}

