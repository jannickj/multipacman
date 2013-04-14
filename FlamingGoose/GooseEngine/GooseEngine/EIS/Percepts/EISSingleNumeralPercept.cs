using System;
using GooseEngine.GameManagement;
using GooseEngine.EIS.Percepts;
using iilang;

namespace GooseEngine.EIS.Percepts
{
	public class EISSingleNumeralPercept : SingleNumeralPercept
	{
		public EISSingleNumeralPercept (string name, double value) : base(name, value)
		{
		}

		#region IEISifiable implementation

		public IILElement EISify ()
		{
			return new IILPercept (Name, new IILNumeral (Value));
		}

		#endregion
	}
}

