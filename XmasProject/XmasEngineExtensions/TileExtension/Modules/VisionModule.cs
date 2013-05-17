using System;
using System.Collections.Generic;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineExtensions.TileExtension.Percepts;
using JSLibrary.Data;
using System.Linq;

namespace XmasEngineExtensions.TileExtension.Modules
{
	public class VisionModule : EntityModule
	{
		private Vision vision;
		private Func<XmasEntity, Vision> visionBuilder;

		public VisionModule (Func<XmasEntity, Vision> visionBuilder)
		{
			this.visionBuilder = visionBuilder;
		}

		public override void AttachToEntity (XmasEntity entityHost, EntityModule replacedModule)
		{
			base.AttachToEntity (entityHost, replacedModule);
			entityHost.Register (new Trigger<UnitMovePostEvent> (xmasEntity_UnitMovedPost));
		}

		public override IEnumerable<Percept> Percepts {
			get {
				return new Percept[] { vision };
			}
		}

		internal void UpdateVision()
		{
			Vision newVision = visionBuilder (entityHost);
			IEnumerable<Tile> oldTiles = vision.VisibleTiles.Values;
			IEnumerable<Tile> newTiles = newVision.VisibleTiles.Values;

			IEnumerable<Tile> persistentTiles = oldTiles.Intersect (newTiles);

			foreach (Tile tile in oldTiles.Except(persistentTiles)) {
				//TODO: disable listening to event on tile
			}

			foreach (Tile tile in newTiles.Except(persistentTiles)) {
				//TODO: listen to event on tile
			}

			vision = newVision;
		}

		private void xmasEntity_UnitMovedPost(UnitMovePostEvent evt)
		{
			UpdateVision ();
		}
	}
}

