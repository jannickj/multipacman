using System;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel;
using System.Collections.Generic;
using XmasEngineModel.Percepts;

namespace ConsoleXmasImplementation.Model.Modules
{
	public class PackageGrabbingModule : EntityModule
	{
		public bool PackageGrabbed { get; set; }

		public PackageGrabbingModule()
		{
			PackageGrabbed = false;
		}

		public PackageGrabbingModule (bool packageGrabbed)
		{
			PackageGrabbed = packageGrabbed;
		}

		public override IEnumerable<Percept> Percepts {
			get {
				if (PackageGrabbed)
					return new Percept[] { new EmptyNamedPercept ("holdingPackage") };

				return new Percept[0];
			}
		}
	}
}

