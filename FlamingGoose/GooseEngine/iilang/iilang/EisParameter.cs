using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace iilang
{
	public abstract class EisParameter : EisIILangElement
	{
		private static Dictionary<string,Type> typeMap = new Dictionary<string, Type>() {
			{"function", typeof(EisFunction)},
			{"identifier", typeof(EisIdentifier)},
			{"number", typeof(EisNumeral)},
			{"parameterList", typeof(EisParameterList)}
		};

		public static EisParameter fromString(string str)
		{
			return Activator.CreateInstance(typeMap [str]) as EisParameter;
		}
	}
}

