using iilang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.EIS.Conversion.IILang.Percepts
{
    public class EISPerceptCollectionSerializer : EISConverterToEIS<PerceptCollection,iilang.IILPerceptCollection>
    {

        public override IILPerceptCollection BeginConversionToForeign(PerceptCollection gobj)
        {
            return new IILPerceptCollection(gobj.Percepts.Select(p => (IILPercept)ConvertToForeign(p)).ToArray());
        }
    }
}
