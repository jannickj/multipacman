using NUnit.Framework;
using XmasEngineModel;
using XmasEngineModel.Conversion;
using XmasEngineModel.Exceptions;

namespace XmasEngine_Test.Model.Conversion
{
	[TestFixture]
	public class GooseConversionToolTest
	{
		private class XmasA : XmasObject
		{
		}

		private class XmasB : XmasA
		{
		}

		private class ForeignA
		{
		}

		private class ForeignB : ForeignA
		{
		}

		private class MockAToAConvert : XmasConverter<XmasA, ForeignA>
		{
			public override XmasA BeginConversionToGoose(ForeignA fobj)
			{
				return new XmasA();
			}

			public override ForeignA BeginConversionToForeign(XmasA gobj)
			{
				return new ForeignA();
			}
		}

		private class MockBToBConvert : XmasConverter<XmasB, ForeignB>
		{
			public override XmasB BeginConversionToGoose(ForeignB fobj)
			{
				return new XmasB();
			}

			public override ForeignB BeginConversionToForeign(XmasB gobj)
			{
				return new ForeignB();
			}
		}

		[Test]
		public void Convert_ForeignBToGooseBWithConverter_ShouldReturnGooseB()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockBToBConvert());

			Assert.IsInstanceOf<XmasB>(ctool.ConvertToGoose(new ForeignB()));
		}

		[Test]
		public void Convert_ForeignBToGooseBWithoutConverter_ShouldNOTUseGooseAToForeignAConverterInstead()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockAToAConvert());

			Assert.Throws<UnconvertableException>(() => ctool.ConvertToGoose(new ForeignB()));
		}

		[Test]
		public void Convert_GooseAToForeignAWithConverter_GoesFromGooseAToForeignA()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockAToAConvert());

			Assert.IsInstanceOf<ForeignA>(ctool.ConvertToForeign(new XmasA()));
		}


		[Test]
		public void Convert_GooseBToForeignBWithoutConverter_ShouldUseGooseAToForeignAConverterInstead()
		{
			XmasConversionTool<ForeignA> ctool = new XmasConversionTool<ForeignA>();
			ctool.AddConverter(new MockAToAConvert());

			Assert.IsInstanceOf<ForeignA>(ctool.ConvertToForeign(new XmasB()));
		}
	}
}