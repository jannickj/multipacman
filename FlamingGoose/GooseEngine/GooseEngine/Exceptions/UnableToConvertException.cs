using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Conversion;

namespace GooseEngine.Exceptions
{
    public class UnableToConvertException : Exception
    {
        private GooseConverter converter;

        public GooseConverter Converter
        {
            get { return converter; }
        }

        public UnableToConvertException(GooseConverter converter)
            : base("Converter: " + converter.GetType().Name + "does not support the conversion")
        {
            this.converter = converter;
        }
    
    }
}
