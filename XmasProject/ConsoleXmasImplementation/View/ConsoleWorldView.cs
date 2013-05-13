using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.View
{
	public class ConsoleWorldView
	{
		private TileWorld model;
		private Dictionary<XmasEntity, ConsoleEntityView> viewlookup = new Dictionary<XmasEntity, ConsoleEntityView>();

		public ConsoleWorldView(TileWorld model)
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
			var locs = new Dictionary<Point, ConsoleEntityView>();
			foreach (KeyValuePair<XmasEntity, ConsoleEntityView> kv in viewlookup)
				locs.Add(((TilePosition)kv.Value.Position).Point, kv.Value);

			return locs;
		}

	
	}
}