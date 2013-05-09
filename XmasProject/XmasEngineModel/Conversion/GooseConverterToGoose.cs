using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class GooseConverterToGoose<GooseType, ForeignType> : GooseConverter<GooseType, ForeignType>
		where GooseType : GooseObject
	{
		public override ForeignType BeginConversionToForeign(GooseType gobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}