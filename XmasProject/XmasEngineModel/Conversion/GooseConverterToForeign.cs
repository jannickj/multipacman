using GooseEngine.Exceptions;

namespace GooseEngine.Conversion
{
	public abstract class GooseConverterToForeign<GooseType, ForeignType> : GooseConverter<GooseType, ForeignType>
		where GooseType : GooseObject
	{
		public override GooseType BeginConversionToGoose(ForeignType fobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}