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

			Size wbSize = this.model.BurstSize;

			foreach (KeyValuePair<XmasEntity, ConsoleEntityView> kv in viewlookup)
			{
				Point p = ((TilePosition) kv.Value.Position).Point;
				Point transp = new Point(p.X + wbSize.Width, p.Y + wbSize.Height);
				

				locs.Add(transp, kv.Value);
			}

			return locs;
		}



	
	}
}