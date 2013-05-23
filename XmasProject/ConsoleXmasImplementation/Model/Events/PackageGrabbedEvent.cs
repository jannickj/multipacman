using System;
using XmasEngineModel.Management;
using XmasEngineModel.EntityLib;

namespace ConsoleXmasImplementation.Model.Events
{
	public class PackageGrabbedEvent : XmasEvent
	{
		public Package GrabbedPackage { get; private set; }

		public PackageGrabbedEvent (Package grabbedPackage)
		{
			GrabbedPackage = grabbedPackage;
		}
	}
}

