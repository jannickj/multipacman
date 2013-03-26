using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Data.GenericEvents
{
    public delegate void ValueHandler<T>(object sender, ValueEvent<T> value);

    public class ValueEvent<T> : EventArgs
    {
        T val;

        public T Value
        {
            get { return val; }
        }

        public ValueEvent(T val)
        {
            this.val = val;

        }



    }
}
