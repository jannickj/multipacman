using System;
using GooseEngine.EIS;

namespace GooseEngine.EIS.Conversion.IILang
{
	public class EISEntitySerializer : EISConverter<Entity,iilang.IILElement>
	{
		#region implemented abstract members of GooseConverter
		public override Entity BeginConversionToGoose (iilang.IILElement fobj)
		{
			throw new NotImplementedException ();
		}
		public override iilang.IILElement BeginConversionToForeign (Entity gobj)
		{
			throw new NotImplementedException ();
		}
		#endregion
    }
}

