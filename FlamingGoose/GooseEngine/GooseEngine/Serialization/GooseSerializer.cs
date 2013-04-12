using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.Serialization
{
    public abstract class GooseSerializer
    {
        public abstract object Serialize(GooseObject gobj);
    }
}
