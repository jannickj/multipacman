using System;

namespace iilang.Exceptions
{
	public class MissingXmlAttributeException : Exception
	{
		public MissingXmlAttributeException(string message)
			: base(message)
		{
		}
	}
}