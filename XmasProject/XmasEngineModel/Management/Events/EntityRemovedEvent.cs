using System;
using XmasEngineModel.Management;
using XmasEngineModel.EntityLib;

namespace XmasEngineModel
{
	public class EntityRemovedEvent : XmasEvent
	{
		public XmasEntity RemovedXmasEntity { get; private set; }

		public EntityRemovedEvent (XmasEntity removedXmasEntity)
		{ 
			RemovedXmasEntity = removedXmasEntity;
		}
	}
}

