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
			EisIdentifier actual_src = new EisIdentifier ("test_id");
			XDocument actual = new XDocument ();

			XmlSerializer serializer = new XmlSerializer(typeof(EisIdentifier));
			serializer.Serialize (actual.CreateWriter (), actual_src);

			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<identifier value=""test_id"" />");

			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void NumeralToXml_SingleNumeral42_ReturnXmlCorrectXml () 
		{
			EisNumeral actual_src = new EisNumeral (42);
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisNumeral));
			serializer.Serialize (actual.CreateWriter (), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<number value=""42"" />");
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void FunctionWithContentToXml_ReturnXmlWithContentAsChildren ()
		{
			EisFunction actual_src = new EisFunction( "test_fun", new EisNumeral(42) );
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisFunction));
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
			EisParameterList actual_src = new EisParameterList (new EisIdentifier ("test_id"), new EisNumeral (42));
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisParameterList));
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

