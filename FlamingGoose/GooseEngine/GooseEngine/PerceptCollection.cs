using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooseEngine
{
    public class PerceptCollection : GooseObject
    {
        private ICollection<Percept> percepts;

        public ICollection<Percept> Percepts
        {
            get { return percepts; }
        }

        public PerceptCollection(ICollection<Percept> percepts)
        {
            this.percepts = percepts;
        }

    }
}
