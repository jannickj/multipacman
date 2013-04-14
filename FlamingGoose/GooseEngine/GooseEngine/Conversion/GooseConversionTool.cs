using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Exceptions;

namespace GooseEngine.Conversion
{
    public class GooseConversionTool<ToType>
    {
        private Dictionary<Type, GooseConverter> converters = new Dictionary<Type, GooseConverter>();

        public GooseConversionTool()
        {
            this.converters.Add(typeof(object), new NoConverter());
        }

        public void AddConverter<FromType>(GooseConverter<FromType,ToType> converter) where FromType : GooseObject
        {
            this.converters.Add(typeof(FromType), converter);          
        }

        public ToType Convert(GooseObject gobj)
        {
            Type original = gobj.GetType();
            Type gt = original;
            GooseConverter converter;
            while (true)
            {
                if (converters.TryGetValue(gt, out converter))
                {
                    if (gt != original)
                    {
                        this.converters.Add(original, new SleekConverter(converter.BeginUnsafeConversion));
                    }
                    return (ToType)converter.BeginUnsafeConversion(gobj);
                }
                else
                    gt = gt.BaseType;
            }
        }


        private class NoConverter : GooseConverter
        {

            internal override object BeginUnsafeConversion(GooseObject gobj)
            {
                throw new UnconvertableException(gobj);
            }
        }

        private class SleekConverter : GooseConverter
        {
            private Func<GooseObject, object> func;

            public SleekConverter(Func<GooseObject, object> func)
            {
                this.func = func;
            }

            internal override object BeginUnsafeConversion(GooseObject gobj)
            {
                return func(gobj);
            }
        }
    }
}
