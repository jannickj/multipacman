using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace iilang
{
	public abstract class IILParameter : IILElement
	{
		private static Dictionary<string,Type> typeMap = new Dictionary<string, Type>() {
			{"function", typeof(IILFunction)},
			{"identifier", typeof(IILIdentifier)},
			{"number", typeof(IILNumeral)},
			{"parameterList", typeof(IILParameterList)}
		};

		public static IILParameter fromString(string str)
		{
			return Activator.CreateInstance(typeMap [str]) as IILParameter;
		}
	}
}

