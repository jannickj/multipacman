using iilang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.EIS.Conversion.IILang
{
    public class EISPerceptCollectionSerializer : EISConverter<PerceptCollection,iilang.IILPerceptCollection>
    {
        public override PerceptCollection BeginConversionToGoose(iilang.IILPerceptCollection fobj)
        {
            throw new NotImplementedException();
        }

        public override IILPerceptCollection BeginConversionToForeign(PerceptCollection gobj)
        {
            return new IILPerceptCollection(gobj.Percepts.Select(p => (IILPercept)ConvertToForeign(p)).ToArray());

        }
    }
}
