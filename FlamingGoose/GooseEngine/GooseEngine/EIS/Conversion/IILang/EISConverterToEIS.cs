using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Conversion;

namespace GooseEngine.EIS.Conversion.IILang
{
    public abstract class EISConverterToEIS<GooseType,EISType> : GooseConverterToForeign<GooseType,EISType> 
        where EISType : iilang.IILElement
        where GooseType : GooseObject
    {

    }
}
