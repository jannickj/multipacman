using System;
using GooseEngine.Conversion;
using iilang;
using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.EIS.Conversion;

namespace GooseEngine.EIS.Conversion.IILang
{
	public abstract class EISActionConverter<ActionType> : EISConverter<ActionType> where ActionType : GameAction
	{
		#region implemented abstract members of GooseConverter
		public override IILElement BeginConversionToForeign (ActionType gobj)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

