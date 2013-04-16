using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.EIS.Conversion.IILang
{
    public abstract class EISPerceptConverter<GooseType> : EISConverterToEIS<GooseType,iilang.IILPercept>
        where GooseType : GooseObject
    {
    }
}
