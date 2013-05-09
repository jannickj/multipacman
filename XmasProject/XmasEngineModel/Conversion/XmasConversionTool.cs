using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class XmasConversionTool
	{
		internal abstract object ConvertToForeignUnsafe(XmasObject gobj);

		internal abstract XmasObject ConvertToGooseUnsafe(object fobj);
	}

	public class XmasConversionTool<ForeignType> : XmasConversionTool
	{
		private Dictionary<Type, XmasConverter> foreignLookup = new Dictionary<Type, XmasConverter>();
		private Dictionary<Type, XmasConverter> gooseLookup = new Dictionary<Type, XmasConverter>();

		public XmasConversionTool()
		{
			NoConverter nocon = new NoConverter();
			gooseLookup.Add(typeof (object), nocon);
			foreignLookup.Add(typeof (object), nocon);
		}

		public virtual void AddConverter<GooseType, ForeignTyped>(XmasConverter<GooseType, ForeignTyped> converter)
			where ForeignTyped : ForeignType
			where GooseType : XmasObject
		{
			converter.ConversionTool = this;

			if (!(converter is XmasConverterToForeign<GooseType, ForeignTyped>))
				foreignLookup.Add(typeof (ForeignTyped), converter);
			if (!(converter is XmasConverterToXmas<GooseType, ForeignTyped>))
				gooseLookup.Add(typeof (GooseType), converter);
		}


		public ForeignType ConvertToForeign(XmasObject gobj)
		{
			Type original = gobj.GetType();
			Type gt = original;
			XmasConverter converter;
			while (true)
			{
				if (gooseLookup.TryGetValue(gt, out converter))
				{
					if (gt != original)
					{
						SleekConverter sleek = new SleekConverter(converter.BeginUnsafeConversionToForeign,
						                                          converter.BeginUnsafeConversionToGoose);
						gooseLookup.Add(original, sleek);
					}
					return (ForeignType) converter.BeginUnsafeConversionToForeign(gobj);
				}
				else
					gt = gt.BaseType;
			}
		}


		internal override object ConvertToForeignUnsafe(XmasObject gobj)
		{
			return ConvertToForeign(gobj);
		}

		internal override XmasObject ConvertToGooseUnsafe(object fobj)
		{
			return ConvertToGoose((ForeignType) fobj);
		}

		public XmasObject ConvertToGoose(ForeignType foreign)
		{
			XmasConverter converter;
			Type ft = foreign.GetType();
			if (foreignLookup.TryGetValue(ft, out converter))
			{
				return converter.BeginUnsafeConversionToGoose(foreign);
			}
			throw new UnconvertableException(foreign);
		}


		private class NoConverter : XmasConverter
		{
			internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
			{
				throw new UnconvertableException(gobj);
			}

			internal override XmasObject BeginUnsafeConversionToGoose(object obj)
			{
				throw new UnconvertableException(obj);
			}
		}

		private class SleekConverter : XmasConverter
		{
			private Func<XmasObject, object> toForeign;
			private Func<object, XmasObject> toGoose;

			public SleekConverter(Func<XmasObject, object> toForeign, Func<object, XmasObject> toGoose)
			{
				this.toForeign = toForeign;
				this.toGoose = toGoose;
			}

			internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
			{
				return toForeign(gobj);
			}

			internal override XmasObject BeginUnsafeConversionToGoose(object obj)
			{
				return toGoose(obj);
			}
		}
	}
}