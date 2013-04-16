using GooseEngine.GameManagement;
using iilang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.EIS.Conversion.IILang
{
    public class EISSingleNumeralSerializer : EISConverter<SingleNumeralPercept, iilang.IILPercept>
    {

        public override SingleNumeralPercept BeginConversionToGoose(IILPercept fobj)
        {
            throw new NotImplementedException();
        }

        public override IILPercept BeginConversionToForeign(SingleNumeralPercept gobj)
        {
            return new IILPercept(gobj.Name, new IILNumeral(gobj.Value));
        }
    }
}
