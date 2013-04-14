using System;
using GooseEngine.Conversion;

namespace GooseEngine.EIS
{
	public abstract class EISConverter<ForeignType> : GooseConverter<GooseObject, ForeignType>
	{
		public EISConverter ()
		{

		}
	}
}

