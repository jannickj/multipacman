using XmasEngineModel.GameManagement.Events;

namespace XmasEngineModel.GameManagement.Actions
{
	public class CloseEngine : EnvironmentAction
	{
		protected override void Execute()
		{
			EventManager.Raise(new EngineCloseEvent());
			Complete();
		}
	}
}