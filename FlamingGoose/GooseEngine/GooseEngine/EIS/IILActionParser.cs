using System;
using iilang;
using GooseEngine.EIS.ActionTypes;

namespace GooseEngine.EIS
{
	public class IILActionParser
	{
		public IILActionParser ()
		{
		}

		public EISAction parseIILAction(IILAction action)
		{
			EISAction retval;

			switch (action.Name) {
			case "getAllPercepts":
				retval = new EISGetAllPercepts();
				break;
			case "moveUnit":
				retval = new EISMoveUnit();
				break;
			default:
				throw new Exception("Unable to parse IIL action named: " + action.Name);
			}

			retval.TransferFrom (action);
			return retval;
		}
	}
}

