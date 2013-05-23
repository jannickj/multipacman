using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineExtensions.TileEisExtension;

namespace ConsoleXmasImplementation.Model.Conversion
{
	class ConsoleConversionTool : TileEisConversionTool
	{
		ConsoleConversionTool()
			: base()
		{
			this.AddConverter(new EISGrabActionConverter());
		}
	}
}
