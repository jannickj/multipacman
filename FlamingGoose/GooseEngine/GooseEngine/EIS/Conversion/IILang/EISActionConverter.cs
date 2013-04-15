using System;
using GooseEngine.Conversion;
using iilang;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.EIS.Conversion;

namespace GooseEngine.EIS.Conversion.IILang
{
	public abstract class EISActionConverter<ActionType,EISActionType> : EISConverter<ActionType,EISActionType> 
        where ActionType : EntityGameAction
        where EISActionType : EISAction
	{
        public override EISActionType BeginConversionToForeign(ActionType gobj)
        {
            throw new NotImplementedException();
        }
	}
}

