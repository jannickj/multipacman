using System;
using iilang;

namespace GooseEngine
{
	public class EISTileSerializer : EISSerializer<Tile>
	{

        public override IILElement BeginConversion(Tile t)
        {
            IILParameterList pl = new IILParameterList();
            foreach (Entity ent in t.Entities)
                pl.AddParameter((IILParameter)this.Convert(ent));

            return pl;
        }
    }
}