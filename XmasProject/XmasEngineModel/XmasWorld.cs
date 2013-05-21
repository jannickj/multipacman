using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Exceptions;
using XmasEngineModel.World;

namespace XmasEngineModel
{
	public abstract class XmasWorld
	{
		private ulong nextId = 1;
		private Dictionary<string,Agent> agentLookup = new Dictionary<string, Agent>();
		private Dictionary<ulong,XmasEntity> entityLookup = new Dictionary<ulong, XmasEntity>(); 

		internal bool FullAddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
			if (xmasEntity is Agent)
			{
				Agent agent = xmasEntity as Agent;
				Agent otheragent;
				if(string.IsNullOrEmpty(agent.Name))
					throw new AgentHasNoNameException(agent);
				else if (agentLookup.TryGetValue(agent.Name, out otheragent))
				{
					throw new AgentAlreadyExistsException(agent, otheragent);
				}
				else
				{
					agentLookup.Add(agent.Name,agent);
				}
			}

			bool entityadded = AddEntity(xmasEntity, info);
			if (entityadded)
			{
				xmasEntity.Id = nextId;
				this.entityLookup.Add(xmasEntity.Id,xmasEntity);
				nextId++;
			}
			return entityadded;
		}

		protected abstract bool AddEntity(XmasEntity xmasEntity, EntitySpawnInformation info);

		public abstract XmasPosition GetEntityPosition(XmasEntity xmasEntity);

		public abstract bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition);

		public bool TryGetAgent(string name, out Agent agent)
		{
			return this.agentLookup.TryGetValue(name, out agent);
		}
	}
}