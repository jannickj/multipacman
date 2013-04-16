using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Exceptions;

namespace GooseEngine.Conversion
{
    public abstract class GooseConversionTool
    {

        internal abstract object ConvertToForeignUnsafe(GooseObject gobj);

        internal abstract GooseObject ConvertToGooseUnsafe(object fobj);
    }
    public class GooseConversionTool<ForeignType> : GooseConversionTool
    {
        private Dictionary<Type, GooseConverter> gooseLookup = new Dictionary<Type, GooseConverter>();
        private Dictionary<Type, GooseConverter> foreignLookup = new Dictionary<Type, GooseConverter>();

        public GooseConversionTool()
        {
            NoConverter nocon = new NoConverter();
            this.gooseLookup.Add(typeof(object), nocon);
            this.foreignLookup.Add(typeof(object), nocon);

        }

        public virtual void AddConverter<GooseType,ForeignTyped>(GooseConverter<GooseType,ForeignTyped> converter) 
            where ForeignTyped : ForeignType
            where GooseType : GooseObject
        {
            converter.ConversionTool = this;

            if(!(converter is GooseConverterToForeign<GooseType,ForeignTyped>))
                this.gooseLookup.Add(typeof(GooseType), converter);
            if(!(converter is GooseConverterToGoose<GooseType,ForeignTyped>))
                this.foreignLookup.Add(typeof(ForeignTyped), converter);
        }


        public ForeignType ConvertToForeign(GooseObject gobj)
        {
            Type original = gobj.GetType();
            Type gt = original;
            GooseConverter converter;
            while (true)
            {
                if (gooseLookup.TryGetValue(gt, out converter))
                {
                    if (gt != original)
                    {
                        SleekConverter sleek = new SleekConverter(converter.BeginUnsafeConversionToForeign,converter.BeginUnsafeConversionToGoose);
                        this.gooseLookup.Add(original, sleek);
                    }
                    return (ForeignType)converter.BeginUnsafeConversionToForeign(gobj);
                }
                else
                    gt = gt.BaseType;
            }
        }


        internal override object ConvertToForeignUnsafe(GooseObject gobj)
        {
            return this.ConvertToForeign(gobj);
        }

        internal override GooseObject ConvertToGooseUnsafe(object fobj)
        {
            return this.ConvertToGoose((ForeignType)fobj);
        }

        public GooseObject ConvertToGoose(ForeignType foreign)
        {
            GooseConverter converter;
            Type ft = foreign.GetType();
            if (foreignLookup.TryGetValue(ft, out converter))
            {
                return converter.BeginUnsafeConversionToGoose((ForeignType)foreign);
            }
            throw new UnconvertableException(foreign);
        }


        private class NoConverter : GooseConverter
        {

            internal override object BeginUnsafeConversionToForeign(GooseObject gobj)
            {
                throw new UnconvertableException(gobj);
            }

            internal override GooseObject BeginUnsafeConversionToGoose(object obj)
            {
                throw new UnconvertableException(obj);
            }
        }

        private class SleekConverter : GooseConverter
        {
            private Func<GooseObject, object> toForeign;
            private Func<object, GooseObject> toGoose;

            public SleekConverter(Func<GooseObject, object> toForeign,Func<object, GooseObject> toGoose)
            {
                this.toForeign = toForeign;
                this.toGoose = toGoose;
            }

            internal override object BeginUnsafeConversionToForeign(GooseObject gobj)
            {
                return toForeign(gobj);
            }

            internal override GooseObject BeginUnsafeConversionToGoose(object obj)
            {
                return toGoose(obj);
            }
        }

    }
}
