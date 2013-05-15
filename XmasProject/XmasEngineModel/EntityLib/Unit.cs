using System;
using System.Collections.Generic;
using System.Linq;
using XmasEngineModel.EntityLib.Module;

namespace XmasEngineModel.EntityLib
{
	public abstract class Unit : XmasEntity
	{

		public Unit()
		{
		}

		public ICollection<Percept> Percepts
		{
			get 
			{ 
				return moduleMap.Values.SelectMany(m => m.Percepts);
			}
		}
	}
}