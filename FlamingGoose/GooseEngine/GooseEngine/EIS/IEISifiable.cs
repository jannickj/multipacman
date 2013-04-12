using System;
using iilang;

namespace GooseEngine
{
	public interface IEISifiable<T>
	{
		IILElement EISify (T obj);
	}
}

