using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Exceptions;

namespace GooseEngine.Conversion
{
    public class GooseConversionTool<ForeignType>
    {
        private Dictionary<Type, GooseConverter> gooseLookup = new Dictionary<Type, GooseConverter>();
        private Dictionary<Type, GooseConverter> foreignLookup = new Dictionary<Type, GooseConverter>();

        public GooseConversionTool()
        {
            NoConverter nocon = new NoConverter();
            this.gooseLookup.Add(typeof(object), nocon);
            this.foreignLookup.Add(typeof(object), nocon);

        }

        public virtual void AddConverter<GooseType>(GooseConverter<GooseType,ForeignType> converter) where GooseType : GooseObject
        {
            this.gooseLookup.Add(typeof(GooseType), converter);
            this.foreignLookup.Add(typeof(ForeignType), converter);
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

        public GooseObject ConvertToGoose(ForeignType foreign)
        {
            GooseConverter converter;
            Type ft = typeof(ForeignType);
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
