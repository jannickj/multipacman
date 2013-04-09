using System;
using NUnit.Framework;
using iilang;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace iilangTest
{
	[TestFixture()]
	public class XmlSerializationTest
	{
		[Test()]
		public void WriteXmlOfIdentifierWithValue_XmlRepresentationOfIdentifierWithValue ()
		{
			EisIdentifier actual_src = new EisIdentifier ("test_id");
            StringBuilder sb = new StringBuilder();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisIdentifier));
			serializer.Serialize (XmlWriter.Create(sb), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<identifier value=""test_id"" />");

            XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void TryWriteXmlOfIdentifierWithoutValue_ThrowException ()
		{
			EisIdentifier actual_src = new EisIdentifier ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisIdentifier));
 
            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}
		
		[Test()]
		public void WriteXmlOfNumeralWithValue_XmlRepresentationOfNumeralWithValue () 
		{
			EisNumeral actual_src = new EisNumeral(42);

            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(actual_src.GetType());
            serializer.Serialize(XmlWriter.Create(sb), actual_src);
			
			XDocument expected = XDocument.Parse(
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<number value=""42"" />");
            
            string actualstr = XDocument.Parse(sb.ToString()).ToString();
			
			Assert.AreEqual (expected.ToString(), actualstr);
		}

		[Test()]
		public void TryWriteXmlOfNumeralWithoutValue_ThrowException ()
		{
			EisNumeral actual_src = new EisNumeral ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisNumeral));

            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test()]
		public void WriteXmlOfParameterListWithContent_ReturnXmlWithContentAsChildren ()
		{
			EisParameterList actual_src = new EisParameterList (new EisIdentifier ("test_id"), new EisNumeral (42));

            StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof(EisParameterList));
			serializer.Serialize (XmlWriter.Create(sb), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<parameterList>
					<identifier value=""test_id"" />
					<number value=""42"" />
				</parameterList>");

            XDocument actual = XDocument.Parse(sb.ToString());
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void WriteXmlOfFunctionWithChildren_ReturnXmlWithContentAsChildren ()
		{
			EisFunction actual_src = new EisFunction( "test_fun", new EisNumeral(42) );

            StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof(EisFunction));
			serializer.Serialize (XmlWriter.Create(sb), actual_src);
			
			XDocument expected = XDocument.Parse (
				@"<?xml version=""1.0"" encoding=""utf-16""?>
				<function name=""test_fun"">
					<number value=""42"" />
				</function>");
            XDocument actual = XDocument.Parse(sb.ToString());

			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void TryWriteXmlOfFunctionWithoutName_ThrowException ()
		{
			EisFunction actual_src = new EisFunction();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisFunction));

            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test()]
		public void WriteXmlOfPerceptWithChildren_ReturnXmlWithRepresentation ()
		{
			EisPercept actual_src = new EisPercept( "test_percept", new EisNumeral(42), new EisIdentifier("test_id") );

            StringBuilder sb = new StringBuilder();
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisPercept));
			serializer.Serialize (XmlWriter.Create(sb), actual_src);
			
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

            XDocument actual = XDocument.Parse(sb.ToString());
			
			Assert.AreEqual (expected.ToString (), actual.ToString ());
		}

		[Test()]
		public void TryWriteXmlOfPerceptWithoutName_ThrowException ()
		{
			EisPercept actual_src = new EisPercept( );
			
			XmlSerializer serializer = new XmlSerializer(typeof(EisPercept));

            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}


        private static XmlWriter GenerateWriter()
        {
            StringBuilder sb = new StringBuilder();
            return XmlWriter.Create(sb);
        }
	}
}

