using System.Collections.Generic;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension.Percepts;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.World;

namespace XmasEngineExtensions.TileExtension
{
	public class TileWorld : XmasWorld
	{
		private Dictionary<XmasEntity, Point> entlocs = new Dictionary<XmasEntity, Point>();
		private TileMap map;

		public TileWorld(TileMap map)
		{
		}

		public TileWorld(Size burstSize)
		{
			this.map = new TileMap(burstSize);			
		}

		public Size Size
		{
			get { return map.Size; }
		}

		public Vision View(Point p, int range, XmasEntity xmasEntity)
		{
			return new Vision(map[p.X, p.Y, range], xmasEntity);
		}

		public Vision View(int range, XmasEntity xmasEntity)
		{
			return View(entlocs[xmasEntity], range, xmasEntity);
		}

		public Vision View(XmasEntity xmasEntity)
		{
			return View(xmasEntity.VisionRange, xmasEntity);
		}

		protected override bool AddEntity(XmasEntity xmasEntity, EntitySpawnInformation info)
		{
			TilePosition tilePos = (TilePosition) info.Position;
			return AddEntity(xmasEntity, tilePos);
		}
		
		private bool AddEntity(XmasEntity xmasEntity, TilePosition pos)
		{
			Point point = pos.Point;
			
			Tile tile = map[point.X, point.Y];

			if (!tile.CanContain(xmasEntity))
				return false;

			entlocs.Add(xmasEntity, point);
			tile.AddEntity(xmasEntity);
			return true;
		}
		
		public override XmasPosition GetEntityPosition(XmasEntity xmasEntity)
		{
			return new TilePosition(entlocs[xmasEntity]);
		}
		
		public override bool SetEntityPosition(XmasEntity xmasEntity, XmasPosition tilePosition)
		{
			return SetEntityPosition (entity, (TilePosition)tilePosition);
		}
		
		private bool SetEntityPosition(XmasEntity xmasEntity, TilePosition pos)
		{
			Point oldPoint;
			bool entityExistsInMap = false;
			
			if (entlocs.TryGetValue (entity, out oldPoint))
				entityExistsInMap = true;
			
			if (!AddEntity (entity, pos))
				return false;
			
			if (entityExistsInMap)
				map [oldPoint].RemoveEntity (entity);
			
			return true;
		}


//
//		internal void SetEntityLocation(Point loc, XmasEntity XmasEntity)
//		{
//			map[loc.X, loc.Y].AddEntity(XmasEntity);
//		}

//		public XmasEntity[] RemoveAllEntities()
//		{
//			XmasEntity[] ents = this.entlocs.Keys.ToArray();
//			foreach (var XmasEntity in ents)
//			{
//				this.RemoveEntity(XmasEntity);
//			}
//			return ents;
//		}

//

	}
}