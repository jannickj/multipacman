using GooseEngine;
using GooseEngine.GameManagement;
using GooseEngine.GameManagement.Events;

namespace GooseEngineView
{
	public class EntityView
	{
		private GooseObject gobj;

		public EntityView(GooseObject gobj, Entity entity)
		{
			this.gobj = gobj;
			entity.Register(new Trigger<UnitMovePreEvent>(unit_Move));
		}

		private void unit_Move(UnitMovePreEvent obj)
		{
		}
	}
}