using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;

namespace iilang
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

		public static IILParameter fromString(string str)
		{
			return Activator.CreateInstance(typeMap [str]) as IILParameter;
		}

        static IILParameter()
        {
            IEnumerable<Type> l = FindAllDerivedTypes<IILParameter>().Where(t => !t.IsAbstract);
            foreach (Type t in l)
            {
                XmlRootAttribute att = t.GetCustomAttributes(typeof(XmlRootAttribute), true).FirstOrDefault() as XmlRootAttribute;
                if (att != null)
                {
                    typeMap.Add(att.ElementName, t);
                }
            }
        }

        private static List<Type> FindAllDerivedTypes<T>()
        {
            return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
        }

        private static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            var derivedType = typeof(T);
            return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t)).ToList();

        }
	}
}

