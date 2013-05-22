using System;
using System.Collections.Generic;
using System.Linq;
using XmasEngineExtensions.TileExtension.Events;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;
using ConsoleXmasImplementation.Model;
using ConsoleXmasImplementation.Model.Events;

namespace XmasEngineExtensions.TileExtension.Actions
{
	public class GrabPackageAction : EntityXmasAction<Agent>
	{
		public GrabPackageAction ()
		{
		}

		protected override void Execute ()
		{
			ICollection<XmasEntity> entities = World.GetEntities (World.GetEntityPosition (Source));

			Package package = entities.OfType<Package>().FirstOrDefault();
			if (package == null) {
				Fail ();
			}
		
			ActionManager.Queue (new RemoveEntityAction (package));

			Complete ();
			Source.Raise (new PackageGrabbedEvent (package));
		}
	}
}

