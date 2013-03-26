using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine.GameManagement.Interfaces
{
    public interface ITrigger
    {
        ICollection<Type> Events
        {
            get;
        }


    }
}
