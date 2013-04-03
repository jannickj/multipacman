using System;
using NUnit.Framework;
using iilang;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace iilangTest
{
	[TestFixture()]
	public class XmlSerializationTest
	{
		[Test()]
		public void WriteXmlOfIdentifierWithValue_XmlRepresentationOfIdentifierWithValue ()
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
		public void TryWriteXmlOfIdentifierWithoutValue_ThrowException ()
		{
			EisIdentifier actual_src = new EisIdentifier ();
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisIdentifier));
			
			Assert.Throws<MissingXmlAttributeException> (() => serializer.Serialize (actual.CreateWriter (), actual_src));
		}
		
		[Test()]
		public void WriteXmlOfNumeralWithValue_XmlRepresentationOfNumeralWithValue () 
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
		public void TryWriteXmlOfNumeralWithoutValue_ThrowException ()
		{
			EisNumeral actual_src = new EisNumeral ();
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisNumeral));

			Assert.Throws<MissingXmlAttributeException> (() => serializer.Serialize (actual.CreateWriter (), actual_src));
		}

		[Test()]
		public void WriteXmlOfParameterListWithContent_ReturnXmlWithContentAsChildren ()
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
		public void WriteXmlOfFunctionWithChildren_ReturnXmlWithContentAsChildren ()
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
		public void TryWriteXmlOfFunctionWithoutName_ThrowException ()
		{
			EisFunction actual_src = new EisFunction();
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisFunction));

			Assert.Throws<MissingXmlAttributeException> (() => serializer.Serialize (actual.CreateWriter (), actual_src));
		}

		[Test()]
		public void WriteXmlOfPerceptWithChildren_ReturnXmlWithRepresentation ()
		{
			EisPercept actual_src = new EisPercept( "test_percept", new EisNumeral(42), new EisIdentifier("test_id") );
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisPercept));
			serializer.Serialize (actual.CreateWriter (), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<percept name=""test_percept"">
					<perceptParameter>
						<number value=""42"" />
					</perceptParameter>
					<perceptParameter>
						<identifier value=""test_id"" />
					</perceptParameter>
				</percept>");
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void TryWriteXmlOfPerceptWithoutName_ThrowException ()
		{
			EisPercept actual_src = new EisPercept( );
			XDocument actual = new XDocument ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisPercept));

			Assert.Throws<MissingXmlAttributeException> (() => serializer.Serialize (actual.CreateWriter (), actual_src));
		}

	}
}

