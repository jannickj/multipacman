using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Data.GenericEvents
{
    public delegate void UnaryValueHandler<T>(object sender, UnaryValueEvent<T> value);

    public class UnaryValueEvent<T> : EventArgs
    {
        private T val;

        public T Value
        {
            get { return val; }
        }

        public UnaryValueEvent(T val)
        {
            this.val = val;
        }
    }
}
