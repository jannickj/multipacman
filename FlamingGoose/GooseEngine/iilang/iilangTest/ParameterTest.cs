using System;
using NUnit.Framework;
using iilang;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace iilangTest
{
	[TestFixture()]
	public class PerceptTest
	{
		[Test()]
		public void IdentifierToXml ()
		{
			Identifier actual_src = new Identifier ("test_id");
			XDocument actual = new XDocument ();

			XmlSerializer serializer = new XmlSerializer(typeof(Identifier));
			serializer.Serialize (actual.CreateWriter (), actual_src);

			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<identifier value=""test_id"" />");

			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void NumeralToXml_SingleNumeral42_ReturnXmlCorrectXml () 
		{
			Numeral actual_src = new Numeral (42);
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(Numeral));
			serializer.Serialize (actual.CreateWriter (), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<number value=""42"" />");
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void FunctionWithContentToXml_ReturnXmlWithContentAsChildren ()
		{
			Function actual_src = new Function( "test_fun", new Numeral(42) );
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(Function));
			serializer.Serialize (actual.CreateWriter (), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<function name=""test_fun"">
					<number value=""42"" />
				</function>");
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void FunctionWithoutContentToXml_ReturnXmlWithoutFunction ()
		{
			Assert.Fail ();
		}

		[Test()]
		public void ParameterListWithContentToXml_ReturnXmlWithContentAsChildren ()
		{
			ParameterList actual_src = new ParameterList (new Identifier ("test_id"), new Numeral (42));
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(ParameterList));
			serializer.Serialize (actual.CreateWriter (), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<parameterList>
					<identifier value=""test_id"" />
					<number value=""42"" />
				</parameterList>");
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void ParameterListWithoutContentToXml_ReturnXmlWithoutParameterList ()
		{
			Assert.Fail ();
		}
	}
}

