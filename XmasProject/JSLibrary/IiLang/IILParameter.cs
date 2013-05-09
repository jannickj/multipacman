using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace JSLibrary.IiLang
{
	public abstract class IILParameter : IILElement
	{
		private static Dictionary<string, Type> typeMap = new Dictionary<string, Type>();
		//{
		//    {"function", typeof(IILFunction)},
		//    {"identifier", typeof(IILIdentifier)},
		//    {"number", typeof(IILNumeral)},
		//    {"parameterList", typeof(IILParameterList)}
		//};

		static IILParameter()
		{
			IEnumerable<Type> l = ExtendedType.FindAllDerivedTypes<IILParameter>().Where(t => !t.IsAbstract);
			foreach (Type t in l)
			{
				XmlRootAttribute att = t.GetCustomAttributes(typeof (XmlRootAttribute), true).FirstOrDefault() as XmlRootAttribute;
				if (att != null)
				{
					typeMap.Add(att.ElementName, t);
				}
			}
		}

		public static IILParameter fromString(string str)
		{
			return Activator.CreateInstance(typeMap[str]) as IILParameter;
		}
	}
}