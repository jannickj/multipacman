using System;
using GooseEngine.Conversion;
using iilang;

namespace GooseEngine.EIS.Conversion
{
	public abstract class EISConverter<GooseType> : GooseConverter<GooseType, IILElement> where GooseType : GooseObject
	{
		public EISConverter ()
		{

		}
	}
}

