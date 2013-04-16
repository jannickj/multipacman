using GooseEngine.GameManagement;
using iilang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.EIS.Conversion.IILang.Percepts
{
    public class EISSingleNumeralSerializer : EISPerceptConverter<SingleNumeralPercept>
    {


        public override IILPercept BeginConversionToForeign(SingleNumeralPercept gobj)
        {
            return new IILPercept(gobj.Name, new IILNumeral(gobj.Value));
        }
    }
}
