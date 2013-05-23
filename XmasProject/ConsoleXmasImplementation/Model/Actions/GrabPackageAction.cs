using ConsoleXmasImplementation.Model.Entities;
using ConsoleXmasImplementation.Model.Events;
using ConsoleXmasImplementation.Model.Modules;
using System.Collections.Generic;
using System.Linq;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Actions;

namespace ConsoleXmasImplementation.Model.Actions
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

			Source.Module<PackageGrabbingModule> ().PackageGrabbed = true;

			ActionManager.Queue (new RemoveEntityAction (package));

			Complete ();
			Source.Raise (new PackageGrabbedEvent (package));
		}
	}
}

