using System.Collections.Generic;

namespace XmasEngineModel
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