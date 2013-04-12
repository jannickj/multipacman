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

    public abstract class GooseSerializer<T> : GooseSerializer where T : GooseObject
    {
		
		public abstract object Serialize(T gobj);
		public override object Serialize (GooseObject gobj)
		{
			return Serialize(gobj);
		}

    }
}
