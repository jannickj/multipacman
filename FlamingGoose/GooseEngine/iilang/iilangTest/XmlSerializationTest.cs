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
			IILIdentifier actual_src = new IILIdentifier ("test_id");
            StringBuilder sb = new StringBuilder();
			
			XmlSerializer serializer = new XmlSerializer(typeof(IILIdentifier));
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
			IILIdentifier actual_src = new IILIdentifier ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(IILIdentifier));
 
            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}
		
		[Test()]
		public void WriteXmlOfNumeralWithValue_XmlRepresentationOfNumeralWithValue () 
		{
			IILNumeral actual_src = new IILNumeral(42);

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
			IILNumeral actual_src = new IILNumeral ();
			
			XmlSerializer serializer = new XmlSerializer(typeof(IILNumeral));

            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test()]
		public void WriteXmlOfParameterListWithContent_ReturnXmlWithContentAsChildren ()
		{
			IILParameterList actual_src = new IILParameterList (new IILIdentifier ("test_id"), new IILNumeral (42));

            StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof(IILParameterList));
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
			IILFunction actual_src = new IILFunction( "test_fun", new IILNumeral(42) );

            StringBuilder sb = new StringBuilder();

			XmlSerializer serializer = new XmlSerializer(typeof(IILFunction));
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
			IILFunction actual_src = new IILFunction();
			
			XmlSerializer serializer = new XmlSerializer(typeof(IILFunction));

            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}

		[Test()]
		public void WriteXmlOfPerceptWithChildren_ReturnXmlWithRepresentation ()
		{
			IILPercept actual_src = new IILPercept( "test_percept", new IILNumeral(42), new IILIdentifier("test_id") );

            StringBuilder sb = new StringBuilder();
			
			XmlSerializer serializer = new XmlSerializer(typeof(IILPercept));
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
			IILPercept actual_src = new IILPercept( );
			
			XmlSerializer serializer = new XmlSerializer(typeof(IILPercept));

            Assert.Catch<Exception>(() => serializer.Serialize(GenerateWriter(), actual_src));
		}


        private static XmlWriter GenerateWriter()
        {
            StringBuilder sb = new StringBuilder();
            return XmlWriter.Create(sb);
        }
	}
}

