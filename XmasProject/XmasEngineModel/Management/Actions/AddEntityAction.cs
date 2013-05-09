using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.World;

namespace XmasEngineModel.Management.Actions
{
	public class AddEntityAction : XmasAction
	{
		private Entity ent; 
		private EntitySpawnInformation info;

		public AddEntityAction(Entity ent, EntitySpawnInformation info)
		{
			this.ent = ent;
			this.info = info;
		}

		protected override void Execute()
		{
			
			ent.ActionManager = ActionManager;
			ent.EventManager = EventManager;
			ent.World = World;
			ent.Factory = Factory;
			this.World.AddEntity(ent,info);
			
		}
	}
}
