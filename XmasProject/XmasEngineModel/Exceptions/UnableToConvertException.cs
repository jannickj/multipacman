using System;
using GooseEngine.Conversion;

namespace GooseEngine.Exceptions
{
	public class UnableToConvertException : Exception
	{
		private GooseConverter converter;

		public UnableToConvertException(GooseConverter converter)
			: base("Converter: " + converter.GetType().Name + "does not support the conversion")
		{
			this.converter = converter;
		}

		public GooseConverter Converter
		{
			get { return converter; }
		}
	}
}