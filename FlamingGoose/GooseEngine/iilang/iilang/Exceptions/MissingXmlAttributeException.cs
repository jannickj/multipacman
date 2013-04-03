using System;

namespace iilang
{
	public class MissingXmlAttributeException : Exception
	{
		public MissingXmlAttributeException(string message)
			: base(message) 
		{ }
	}
}

