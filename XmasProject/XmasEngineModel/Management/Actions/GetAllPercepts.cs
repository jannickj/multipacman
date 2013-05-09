using XmasEngineModel.Entities;
using XmasEngineModel.Management.Events;

namespace XmasEngineModel.Management.Actions
{
	public class GetAllPercepts : EntityXmasAction<Unit>
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