using System;
using iilang;
using GooseEngine.EIS;

namespace GooseEngine.EIS.Conversion.IILang.Objects
{
    public class EISTileSerializer : EISConverterToEIS<Tile,IILElement>
	{


        public override IILElement BeginConversionToForeign(Tile t)
        {
            IILParameterList pl = new IILParameterList();
            foreach (Entity ent in t.Entities)
                pl.AddParameter((IILParameter)this.ConvertToForeign(ent));

            return pl;
        }
    }
}