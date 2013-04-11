using System;
using GooseEngine.GameManagement;
using GooseEngine.EIS.Percepts;
using iilang;

namespace GooseEngine.EIS.Percepts
{
	public class EISSingleNumeralPercept : SingleNumeralPercept, IEISPercept
	{
		public EISSingleNumeralPercept (string name, double value) : base(name, value)
		{
		}

		#region IEISPercept implementation
		public iilang.IILPercept toIILang ()
		{
			return new IILPercept (Name, new IILNumeral (Value));
		}
		#endregion
	}
}

