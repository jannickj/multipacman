using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Exceptions
{
    public class UnconvertableException : Exception
    {
        private GooseObject gobj;

        public GooseObject ConvertingObject
        {
            get { return gobj; }
        }


        public UnconvertableException(GooseObject gobj) : base("Conversion for object of type: "+gobj.GetType().Name+" Was not possible")
        {
            this.gobj = gobj;
        }
    }
}
