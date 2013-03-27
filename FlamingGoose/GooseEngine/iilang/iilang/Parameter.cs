using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace iilang
{
	public abstract class Parameter : IILangElement
	{
		private static Dictionary<string,Type> typeMap = new Dictionary<string, Type>() {
			{"function", typeof(Function)},
			{"identifier", typeof(Identifier)},
			{"number", typeof(Numeral)},
			{"parameterList", typeof(ParameterList)}
		};

		public static Parameter fromString(string str)
		{
			return Activator.CreateInstance(typeMap [str]) as Parameter;
		}
	}
}

