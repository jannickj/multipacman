using System;
using GooseEngine.Conversion;

namespace GooseEngine
{
	public abstract class EISSerializer<T> : GooseConverter<T,iilang.IILElement> where T : GooseObject
	{

	}
}

