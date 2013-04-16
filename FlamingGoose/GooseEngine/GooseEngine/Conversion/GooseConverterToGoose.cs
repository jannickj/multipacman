using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Exceptions;

namespace GooseEngine.Conversion
{
    public abstract class GooseConverterToGoose<GooseType, ForeignType> : GooseConverter<GooseType, ForeignType>
         where GooseType : GooseObject
    {

        public override ForeignType BeginConversionToForeign(GooseType gobj)
        {
            throw new UnableToConvertException(this);
        }
    }
}
