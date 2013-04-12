using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Serialization;

namespace GooseEngine
{
    public class GooseObject
    {
        private GooseSerializer serializer;

        internal GooseSerializer Serializer
        {
            set
            {
                this.serializer = value;
            }

        }

        public object toSerializableObject()
        {
            return serializer.Serialize(this);
        }

    }
}
