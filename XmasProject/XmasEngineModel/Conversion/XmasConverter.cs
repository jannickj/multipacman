namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverter
	{
		internal abstract object BeginUnsafeConversionToForeign(XmasObject gobj);
		internal abstract XmasObject BeginUnsafeConversionToXmas(object obj);
	}

	public abstract class XmasConverter<XmasType, ForeignType> : XmasConverter where XmasType : XmasObject
	{
		private XmasConversionTool conversionTool;

		internal XmasConversionTool ConversionTool
		{
			private get { return conversionTool; }
			set { conversionTool = value; }
		}


		public abstract XmasType BeginConversionToXmas(ForeignType fobj);

		public abstract ForeignType BeginConversionToForeign(XmasType gobj);


		protected object ConvertToForeign(XmasObject gobj)
		{
			return conversionTool.ConvertToForeignUnsafe(gobj);
		}

		protected XmasObject ConvertToXmas(ForeignType fobj)
		{
			return conversionTool.ConvertToXmasUnsafe(fobj);
		}

		internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
		{
			return BeginConversionToForeign((XmasType) gobj);
		}

		internal override XmasObject BeginUnsafeConversionToXmas(object obj)
		{
			return BeginConversionToXmas((ForeignType) obj);
		}
	}
}