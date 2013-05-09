using System;
using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineModel;

namespace ConsoleXmasImplementation
{
	public class ConsoleWorldView
	{
		private XmasWorld model;
		private Dictionary<Entity, ConsoleEntityView> viewlookup = new Dictionary<Entity, ConsoleEntityView>();

		public ConsoleWorldView(XmasWorld model)
		{
			this.model = model;
		}

		//public int Width
		//{
		//	get { return model.Size.Width; }
		//}

		//public int Height
		//{
		//	get { return model.Size.Height; }
		//}

		public void AddEntity(ConsoleEntityView entview)
		{
			viewlookup.Add(entview.Model, entview);
		}

		public Dictionary<Point, ConsoleEntityView> AllEntities()
		{
			Dictionary<Point, ConsoleEntityView> locs = new Dictionary<Point, ConsoleEntityView>();
			foreach (KeyValuePair<Entity, ConsoleEntityView> kv in viewlookup)
				locs.Add(kv.Value.Position, kv.Value);

			return locs;
		}

		public void gooseWorld_EntityAdded(object sender, EventArgs e)
		{

		}
	}
}