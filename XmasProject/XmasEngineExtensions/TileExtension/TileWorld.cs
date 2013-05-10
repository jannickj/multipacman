using System;
using System.Collections.Generic;
using JSLibrary.Data;
using System.Linq;
using XmasEngineModel;
using XmasEngineModel.Perceptions;
using XmasEngineModel.World;
using XmasEngineExtensions.TileExtension;

namespace XmasEngineExtensions.TileExtension
{
	public class TileWorld : XmasWorld
	{
		private Dictionary<Entity, Point> entlocs = new Dictionary<Entity, Point>();
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

		public Vision View(Point p, int range, Entity entity)
		{
			return new Vision(map[p.X, p.Y, range], entity);
		}

		public Vision View(int range, Entity entity)
		{
			return View(entlocs[entity], range, entity);
		}

		public Vision View(Entity entity)
		{
			return View(entity.VisionRange, entity);
		}

		protected override bool AddEntity(Entity entity, EntitySpawnInformation info)
		{
			TilePosition tilePos = (TilePosition) info.Position;
			return AddEntity (entity, tilePos);
		}
		
		private bool AddEntity(Entity entity, TilePosition pos)
		{
			Point point = pos.Point;
			
			Tile tile = map [point.X, point.Y];
			
			if (!tile.CanContain(entity))
				return false;
			
			entlocs.Add (entity, point);
			tile.AddEntity (entity);
			return true;
		}
		
		public override XmasPosition GetEntityPosition(Entity entity)
		{
			return new TilePosition (entlocs [entity]);
		}
		
		public override bool SetEntityPosition(Entity entity, XmasPosition tilePosition)
		{
			return SetEntityPosition (entity, (TilePosition)tilePosition);
		}
		
		private bool SetEntityPosition (Entity entity, TilePosition pos)
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
//		internal void SetEntityLocation(Point loc, Entity entity)
//		{
//			map[loc.X, loc.Y].AddEntity(entity);
//		}

//		public Entity[] RemoveAllEntities()
//		{
//			Entity[] ents = this.entlocs.Keys.ToArray();
//			foreach (var entity in ents)
//			{
//				this.RemoveEntity(entity);
//			}
//			return ents;
//		}

//

	}
}