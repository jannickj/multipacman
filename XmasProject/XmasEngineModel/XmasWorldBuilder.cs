using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;
using XmasEngineModel.World;

namespace XmasEngineModel
{
	public abstract class XmasWorldBuilder
	{
		private List<XmasAction> buildactions = new List<XmasAction>();

		public void AddEntity(XmasEntity ent, EntitySpawnInformation info)
		{
			buildactions.Add(new AddEntityAction(ent, info));
		}

		internal void Build(ActionManager actman)
		{
			foreach (XmasAction buildaction in buildactions.ToArray())
			{
				actman.QueueAction(buildaction);
			}
		}
	}
}