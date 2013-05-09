namespace GooseEngine.GameManagement.Events
{
	public class RetreivePerceptsEvent : GameEvent
	{
		private PerceptCollection percepts;

		public RetreivePerceptsEvent(PerceptCollection percepts)
		{
			this.percepts = percepts;
		}

		public PerceptCollection Percepts
		{
			get { return percepts; }
		}
	}
}