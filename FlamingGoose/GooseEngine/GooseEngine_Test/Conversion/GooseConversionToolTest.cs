﻿using System;
using GooseEngine;
using GooseEngine.Conversion;
using GooseEngine.Exceptions;
using NUnit.Framework;


namespace GooseEngine_Test.Conversion
{
    [TestFixture]
    public class GooseConversionToolTest
    {


        [Test]
        public void Convert_GooseAToForeignAWithConverter_GoesFromGooseAToForeignA()
        {
            GooseConversionTool<ForeignA> ctool = new GooseConversionTool<ForeignA>();
            ctool.AddConverter(new MockAToAConvert());

            Assert.IsInstanceOf<ForeignA>(ctool.ConvertToForeign(new GooseA()));

        }


        [Test]
        public void Convert_GooseBToForeignBWithoutConverter_ShouldUseGooseAToForeignAConverterInstead()
        {
            GooseConversionTool<ForeignA> ctool = new GooseConversionTool<ForeignA>();
            ctool.AddConverter(new MockAToAConvert());

            Assert.IsInstanceOf<ForeignA>(ctool.ConvertToForeign(new GooseB()));

        }


        [Test]
        public void Convert_ForeignBToGooseBWithoutConverter_ShouldNOTUseGooseAToForeignAConverterInstead()
        {
            GooseConversionTool<ForeignA> ctool = new GooseConversionTool<ForeignA>();
            ctool.AddConverter(new MockAToAConvert());

            Assert.Throws<UnconvertableException>(() => ctool.ConvertToGoose(new ForeignB()));

        }

        [Test]
        public void Convert_ForeignBToGooseBWithConverter_ShouldReturnGooseB()
        {
            GooseConversionTool<ForeignA> ctool = new GooseConversionTool<ForeignA>();
            ctool.AddConverter(new MockBToBConvert());

            Assert.IsInstanceOf<GooseB>(ctool.ConvertToGoose(new ForeignB()));

        }

        #region MOCKS
        private class GooseA : GooseObject
        {

        }

        private class GooseB : GooseA
        {

        }

        private class ForeignA
        {

        }

        private class ForeignB : ForeignA
        {

        }

        private class MockAToAConvert : GooseConverter<GooseA, ForeignA>
        {

            public override GooseA BeginConversionToGoose(ForeignA fobj)
            {
                return new GooseA();
            }

            public override ForeignA BeginConversionToForeign(GooseA gobj)
            {
                return new ForeignA();
            }
        }

        private class MockBToBConvert : GooseConverter<GooseB, ForeignB>
        {


            public override GooseB BeginConversionToGoose(ForeignB fobj)
            {
                return new GooseB();
            }

            public override ForeignB BeginConversionToForeign(GooseB gobj)
            {
                return new ForeignB();
            }
        }
        #endregion
    }
}