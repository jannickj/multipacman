using GooseEngine.GameManagement.Events;

namespace GooseEngine.GameManagement.Actions
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