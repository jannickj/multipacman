using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using JSLibrary.IiLang.DataContainers;
using JSLibrary.IiLang.Exceptions;
using JSLibrary.IiLang.Parameters;
using NUnit.Framework;

namespace JSLibrary_Test.IiLang
{
	[TestFixture]
	public class XmlDeserializationTest
	{
		public void TryReadXmlOfIdentifierWithoutValue_ThrowException()
		{
			XmlSerializer serializer = new XmlSerializer(typeof (IILIdentifier));

			XElement actual_src = XElement.Parse(@"<identifier />");
			Assert.Throws<MissingXmlAttributeException>(() => serializer.Deserialize(actual_src.CreateReader()));
		}

		[Test]
		public void ReadXmlOfFunctionWithChildren_EisFunctionObjectWithChildren()
		{
			IILFunction expected = new IILFunction("test_fun", new IILIdentifier("test_id"), new IILNumeral(42));

			XmlSerializer serializer = new XmlSerializer(typeof (IILFunction));

			XElement actual_src = XElement.Parse(
				@"<function name=""test_fun"">
					<identifier value=""test_id"" />
					<number value=""42"" />
				</function>");

			IILFunction actual = (IILFunction) serializer.Deserialize(actual_src.CreateReader());
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ReadXmlRepresentationOfAction_moveToAction_ReturnsCorrectMovetToActionObject()
		{
			IILAction expected = new IILAction("moveTo", new IILNumeral(2), new IILNumeral(3));

			XmlSerializer serializer = new XmlSerializer(typeof (IILAction));

			XElement actual_src = XElement.Parse(
				@"<action name=""moveTo"">
					<actionParameter>
						<number value=""2.0"" />
					</actionParameter>
					<actionParameter>
						<number value=""3.0"" />
					</actionParameter>
				</action>");

			IILAction actual = (IILAction) serializer.Deserialize(actual_src.CreateReader());
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TryReadXmlOfFunctionWithoutName_ThrowException()
		{
			XmlSerializer serializer = new XmlSerializer(typeof (IILFunction));

			XElement actual_src = XElement.Parse(
				@"<function>
					<identifier value=""test_id"" />
					<number value=""42"" />
				</function>");

			Assert.Catch<Exception>(() => serializer.Deserialize(actual_src.CreateReader()));
		}

		[Test]
		public void TryReadXmlOfIdentifierWithValue_EisIdentifierObject()
		{
			IILIdentifier expected = new IILIdentifier("test_id");

			XmlSerializer serializer = new XmlSerializer(typeof (IILIdentifier));

			XElement actual_src = XElement.Parse(@"<identifier value=""test_id"" />");

			IILIdentifier actual = (IILIdentifier) serializer.Deserialize(actual_src.CreateReader());
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TryReadXmlOfNumeralWithValue_EisNumeralObject()
		{
			IILNumeral expected = new IILNumeral(42);

			XmlSerializer serializer = new XmlSerializer(typeof (IILNumeral));

			XElement actual_src = XElement.Parse(@"<number value=""42"" />");

			IILNumeral actual = (IILNumeral) serializer.Deserialize(actual_src.CreateReader());
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void TryReadXmlOfNumeralWithoutValue_ThrowException()
		{
			XmlSerializer serializer = new XmlSerializer(typeof (IILNumeral));

			XElement actual_src = XElement.Parse(@"<number />");
			Assert.Catch<Exception>(() => serializer.Deserialize(actual_src.CreateReader()));
		}

		[Test]
		public void TryReadXmlRepresentationOfActionWithoutName_ActionObject()
		{
			XmlSerializer serializer = new XmlSerializer(typeof (IILAction));

			XElement actual_src = XElement.Parse(
				@"<action>
					<actionParameter>
						<number value=""2.0"" />
					</actionParameter>
					<actionParameter>
						<number value=""3.0"" />
					</actionParameter>
				</action>");

			Assert.Catch<Exception>(() => serializer.Deserialize(actual_src.CreateReader()));
		}
	}
}