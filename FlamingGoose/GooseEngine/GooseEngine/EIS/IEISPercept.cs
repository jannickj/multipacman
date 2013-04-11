using System;
using GooseEngine;
using iilang;

namespace GooseEngine.EIS.Percepts
{
	public interface EISPercept : Percept
	{
		EisPercept toIILang ();
	}
}

