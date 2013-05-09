using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;
using XmasEngineModel.World;

namespace XmasEngineModel
{
	public abstract class XmasWorldBuilder
	{
		private List<XmasAction> buildactions = new List<XmasAction>();

		public void AddEntity(Entity ent, EntitySpawnInformation info)
		{
			this.buildactions.Add(new AddEntityAction(ent, info));
		}

		internal void Build(ActionManager actman)
		{
			foreach (var buildaction in buildactions.ToArray())
			{
				actman.QueueAction(buildaction);
			}
		}
	}
}
