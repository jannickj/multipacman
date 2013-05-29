using System.Collections.Generic;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Exceptions;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineModel.World;

namespace XmasEngineModel
{
	public abstract class XmasWorld
	{
		private ulong nextId = 1;
		private Dictionary<string,Agent> agentLookup = new Dictionary<string, Agent>();
		private Dictionary<ulong,XmasEntity> entityLookup = new Dictionary<ulong, XmasEntity>();
		private EventManager evtman;

		public EventManager EventManager
		{
			get { return evtman; }
			internal set { evtman = value; }
		}

		public bool AddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
            xmasEntity.Load();
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

			bool entityadded = OnAddEntity(xmasEntity, info);
			if (entityadded)
			{
          

				xmasEntity.Id = nextId;
				this.entityLookup.Add(xmasEntity.Id,xmasEntity);
				nextId++;

				evtman.Raise(new EntityAddedEvent(xmasEntity,info.Position));

                this.evtman.AddEntity(xmasEntity);
                xmasEntity.OnEnterWorld();
			}

			return entityadded;
		}

		public void RemoveEntity(XmasEntity entity)
		{
			if (entity is Agent) {
				Agent agent = entity as Agent;

				agentLookup.Remove(agent.Name);
			}

			entityLookup.Remove (entity.Id);

			OnRemoveEntity(entity);

			evtman.Raise(new EntityRemovedEvent(entity));

            this.evtman.RemoveEntity(entity);
            entity.OnLeaveWorld();

		}

		protected abstract bool OnAddEntity(XmasEntity xmasEntity, EntitySpawnInformation info);

		protected abstract void OnRemoveEntity(XmasEntity entity);

		public abstract XmasPosition GetEntityPosition(XmasEntity xmasEntity);

		public abstract bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition);

		public bool TryGetAgent(string name, out Agent agent)
		{
			return this.agentLookup.TryGetValue(name, out agent);
		}

		public abstract ICollection<XmasEntity> GetEntities (XmasPosition pos);
	}
}