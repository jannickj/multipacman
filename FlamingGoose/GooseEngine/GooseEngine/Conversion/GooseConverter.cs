using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Conversion
{
	public abstract class GooseConverter
	{
        internal abstract object BeginUnsafeConversion(GooseObject gobj);
	}

    public abstract class GooseConverter<FromType,ToType> : GooseConverter where FromType : GooseObject
    {

        private GooseConversionTool<ToType> conversionTool;

        internal GooseConversionTool<ToType> ConversionTool
        {
            private get { return conversionTool; }
            set { conversionTool = value; }
        }


		public abstract ToType BeginConversion(FromType gobj);


        protected ToType Convert(GooseObject gobj)
        {
            return conversionTool.Convert(gobj);
        }


        internal override object BeginUnsafeConversion(GooseObject gobj)
        {
            return this.BeginConversion((FromType)gobj);
        }
    }
}
