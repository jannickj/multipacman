using System.Collections.Generic;
using GooseEngine;
using JSLibrary.Data;

namespace GooseEngineView.Console
{
	public class ConsoleWorldView
	{
		private GooseWorld model;
		private Dictionary<Entity, ConsoleEntityView> viewlookup = new Dictionary<Entity, ConsoleEntityView>();

		public ConsoleWorldView(GooseWorld model)
		{
			this.model = model;
		}

		public int Width
		{
			get { return model.Size.Width; }
		}

		public int Height
		{
			get { return model.Size.Height; }
		}

		public void AddEntity(ConsoleEntityView entview)
		{
			viewlookup.Add(entview.Model, entview);
		}


		public Dictionary<Point, ConsoleEntityView> AllEntities()
		{
			Dictionary<Point, ConsoleEntityView> locs = new Dictionary<Point, ConsoleEntityView>();
			foreach (KeyValuePair<Entity, ConsoleEntityView> kv in viewlookup)
				locs.Add(kv.Value.Position, kv.Value);

			return null;
		}
	}
}