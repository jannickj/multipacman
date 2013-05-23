using System;
using XmasEngineModel;

namespace ConsoleXmasImplementation.Model.Percepts
{
	public class PackageHoldingPercept : Percept
	{
		public bool HasPackage { get; private set; }
		public string Name { get { return "holdingPackage"; } }

		public PackageHoldingPercept (bool hasPackage)
		{
			HasPackage = hasPackage;
		}
	}
}

