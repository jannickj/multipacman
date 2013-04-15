﻿using GooseEngine.Data;
using GooseEngine.EIS.EISPercepts;
using GooseEngine.Percepts;
using iilang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.EIS.Conversion.IILang
{
    public class EISVisionSerializer : EISConverter<Vision,IILPercept>
    {

        public override Vision BeginConversionToGoose(IILPercept fobj)
        {
            throw new NotImplementedException();
        }

        public override IILPercept BeginConversionToForeign(Vision gobj)
        {
            IILPercept percept = new IILPercept("vision");

            foreach (KeyValuePair<Point, Tile> kvp in gobj.VisibleTiles)
            {
                IILFunction fun = new IILFunction("on",
                                                   new IILNumeral(kvp.Key.X),
                                                   new IILNumeral(kvp.Key.Y),
                                                   new IILIdentifier(kvp.Value.ToString())
                                                   );
                percept.addParameter(fun);
            }

            return percept;
        }
    }
}
