using System.Collections.Generic;
using System.Linq;

namespace XmasEngineModel.EntityLib
{
	public class Agent : XmasEntity
	{
        private string name;

        public Agent(string name)
        {
            this.name = name;
        }

		public string Name
		{
			get { return name; }
			protected set { name = value; }
		}

		public IEnumerable<Percept> Percepts
		{
			get 
			{ 
				return moduleMap.Values.SelectMany(m => m.Percepts);
			}
		}

	}
}