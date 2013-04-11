using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooseEngine.Data;
using GooseEngine.Entities.MapEntities;
using GooseEngine.Percepts;

namespace GooseEngine.GameManagement
{
	public class SingleNumeralPercept : IPercept
	{
		private string name;
		private double value;

		public string Name {
			get { return name; }
		}

		public double Value {
			get { return value; }
		}

		public SingleNumeralPercept (string name, double value)
		{
			this.name = name;
			this.value = value;
		}
	}

}
