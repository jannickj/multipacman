using System;
using GooseEngine.Conversion;
using iilang;

namespace GooseEngine.EIS.Conversion
{
	public abstract class EISConverter<GooseType, EISType> : GooseConverter<GooseType, EISType> 
        where GooseType : GooseObject
        where EISType : IILElement
	{
		public EISConverter ()
		{

		}
	}
}

