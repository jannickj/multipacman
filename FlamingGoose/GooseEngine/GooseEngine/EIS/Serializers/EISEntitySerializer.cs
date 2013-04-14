using System;
using GooseEngine.EIS;

namespace GooseEngine
{
	public class EISEntitySerializer : EISConverter<Entity>
	{
		#region implemented abstract members of GooseConverter

		public override GooseObject BeginConversionToGoose (Entity gobj)
		{
			throw new NotImplementedException ();
		}

		public override Entity BeginConversionToForeign (GooseObject gobj)
		{
			throw new NotImplementedException ();
		}

		#endregion
    }
}

