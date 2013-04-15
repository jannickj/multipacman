using System;
using GooseEngine.Entities;
using GooseEngine.GameManagement.Events;

namespace GooseEngine.GameManagement.Actions
{
	public class GetAllPercepts : EntityGameAction<Unit>
	{
		public GetAllPercepts ()
		{
		}

		#region implemented abstract members of GameAction

		protected override void Execute ()
		{
			this.Source.Raise (new RetreivePerceptsEvent (new PerceptCollection(this.Source.Percepts))); 
			this.Complete ();
		}

		#endregion
	}
}

