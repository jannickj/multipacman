using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Conversion
{
	public abstract class GooseConverter
	{
        internal abstract object BeginUnsafeConversionToForeign(GooseObject gobj);
        internal abstract GooseObject BeginUnsafeConversionToGoose(object obj);
	}

    public abstract class GooseConverter<GooseType,ForeignType> : GooseConverter where GooseType : GooseObject
    {

        private GooseConversionTool conversionTool;

        internal GooseConversionTool ConversionTool
        {
            private get { return conversionTool; }
            set { conversionTool = value; }
        }


        public abstract GooseType BeginConversionToGoose(ForeignType fobj);

        public abstract ForeignType BeginConversionToForeign(GooseType gobj);


        protected object ConvertToForeign(GooseObject gobj)
        {
            return conversionTool.ConvertToForeignUnsafe(gobj);
        }

		protected GooseObject ConvertToGoose(ForeignType fobj)
		{
			return conversionTool.ConvertToGooseUnsafe(fobj);
		}

        internal override object BeginUnsafeConversionToForeign(GooseObject gobj)
        {
            return this.BeginConversionToForeign((GooseType)gobj);
        }

		internal override GooseObject BeginUnsafeConversionToGoose (object obj)
		{
			return this.BeginConversionToGoose ((ForeignType)obj);
		}
    }
}
