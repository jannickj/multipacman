using System.Collections.Generic;

namespace GooseEngine
{
	public class PerceptCollection : GooseObject
	{
		private ICollection<Percept> percepts;

		public PerceptCollection(ICollection<Percept> percepts)
		{
			this.percepts = percepts;
		}

		public ICollection<Percept> Percepts
		{
			get { return percepts; }
		}
	}
}