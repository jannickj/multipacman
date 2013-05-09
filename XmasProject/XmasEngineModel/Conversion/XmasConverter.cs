namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverter
	{
		internal abstract object BeginUnsafeConversionToForeign(XmasObject gobj);
		internal abstract XmasObject BeginUnsafeConversionToGoose(object obj);
	}

	public abstract class XmasConverter<GooseType, ForeignType> : XmasConverter where GooseType : XmasObject
	{
		private XmasConversionTool conversionTool;

		internal XmasConversionTool ConversionTool
		{
			private get { return conversionTool; }
			set { conversionTool = value; }
		}


		public abstract GooseType BeginConversionToGoose(ForeignType fobj);

		public abstract ForeignType BeginConversionToForeign(GooseType gobj);


		protected object ConvertToForeign(XmasObject gobj)
		{
			return conversionTool.ConvertToForeignUnsafe(gobj);
		}

		protected XmasObject ConvertToGoose(ForeignType fobj)
		{
			return conversionTool.ConvertToGooseUnsafe(fobj);
		}

		internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
		{
			return BeginConversionToForeign((GooseType) gobj);
		}

		internal override XmasObject BeginUnsafeConversionToGoose(object obj)
		{
			return BeginConversionToGoose((ForeignType) obj);
		}
	}
}