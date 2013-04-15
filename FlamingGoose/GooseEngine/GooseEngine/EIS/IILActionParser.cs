using System;
using iilang;
using GooseEngine.EIS.ActionTypes;
using System.Collections.Generic;

namespace GooseEngine.EIS
{
	public class IILActionParser
	{
        private Dictionary<string, Type> convert = new Dictionary<string, Type>();

		public IILActionParser ()
		{
            this.Add<EISGetAllPercepts>("getAllPercepts");
            this.Add<EISMoveUnit>("moveUnit");

		}
        
        public void Add<EISActionType>(string actionName) where EISActionType : EISAction
        {
            this.convert.Add(actionName, typeof(EISActionType));
        }

		public EISAction parseIILAction(IILAction action)
		{
			EISAction retval;
            Type actiontype;

            if (convert.TryGetValue(action.Name, out actiontype))
            {
                retval = (EISAction)Activator.CreateInstance(actiontype);
                retval.TransferFrom(action);
                return retval;
            }
            else
                throw new Exception("Unable to parse IIL action named: " + action.Name);
		}
	}
}

