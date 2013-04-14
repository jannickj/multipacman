using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Exceptions
{
    public class UnconvertableException : Exception
    {
        private object gobj;

        public object ConvertingObject
        {
            get { return gobj; }
        }


        public UnconvertableException(object gobj) : base("Conversion for object of type: "+gobj.GetType().Name+" Was not possible")
        {
            this.gobj = gobj;
        }
    }
}
