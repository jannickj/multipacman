using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement
{
    public abstract class EntityGameAction<T> : GameAction where T : Entity
    {
        private T source;

        public T Source
        {
            get
            {
                return source;
            }
            internal set
            {
                source = value;
            }
        }

    }
}
