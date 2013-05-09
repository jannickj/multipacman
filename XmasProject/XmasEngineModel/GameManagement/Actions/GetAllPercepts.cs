using XmasEngineModel.Entities;
using XmasEngineModel.GameManagement.Events;

namespace XmasEngineModel.GameManagement.Actions
{
	public class GetAllPercepts : EntityGameAction<Unit>
	{
		#region implemented abstract members of GameAction

		protected override void Execute()
		{
			Source.Raise(new RetreivePerceptsEvent(new PerceptCollection(Source.Percepts)));
			Complete();
		}

		#endregion
	}
}