using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	public abstract class XmasConversionTool
	{
		internal abstract object ConvertToForeignUnsafe(XmasObject gobj);

		internal abstract XmasObject ConvertToXmasUnsafe(object fobj);
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

		public virtual void AddConverter<XmasType, ForeignTyped>(XmasConverter<XmasType, ForeignTyped> converter)
			where ForeignTyped : ForeignType
			where XmasType : XmasObject
		{
			converter.ConversionTool = this;

			if (!(converter is XmasConverterToForeign<XmasType, ForeignTyped>))
				foreignLookup.Add(typeof (ForeignTyped), converter);
			if (!(converter is XmasConverterToXmas<XmasType, ForeignTyped>))
				gooseLookup.Add(typeof (XmasType), converter);
		}


		public ForeignType ConvertToForeign(XmasObject gobj)
		{
			Type original = gobj.GetType();
			Type gt = original;
			XmasConverter converter;
			try
			{
				while (true)
				{
					if (gooseLookup.TryGetValue(gt, out converter))
					{
						if (gt != original)
						{
							SleekConverter sleek = new SleekConverter(converter.BeginUnsafeConversionToForeign,
																	  converter.BeginUnsafeConversionToXmas);
							gooseLookup.Add(original, sleek);
						}
						return (ForeignType)converter.BeginUnsafeConversionToForeign(gobj);
					}
					else
						gt = gt.BaseType;
				}
			}
			catch (Exception inner)
			{
				throw new UnconvertableException(gobj, inner);
			}
			
		}


		internal override object ConvertToForeignUnsafe(XmasObject gobj)
		{
			return ConvertToForeign(gobj);
		}

		internal override XmasObject ConvertToXmasUnsafe(object fobj)
		{
			return ConvertToXmas((ForeignType) fobj);
		}

		public XmasObject ConvertToXmas(ForeignType foreign)
		{
			XmasConverter converter;
			try
			{
				Type ft = foreign.GetType();
				if (foreignLookup.TryGetValue(ft, out converter))
				{
					return converter.BeginUnsafeConversionToXmas(foreign);
				}
				throw new KeyNotFoundException("Converter not found for "+foreign.GetType().Name);
			}
			catch (Exception inner)
			{
				throw new UnconvertableException(foreign, inner);
				
			}
			
		}


		private class NoConverter : XmasConverter
		{
			internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
			{
				throw new UnconvertableException(gobj);
			}

			internal override XmasObject BeginUnsafeConversionToXmas(object obj)
			{
				throw new UnconvertableException(obj);
			}
		}

		private class SleekConverter : XmasConverter
		{
			private Func<XmasObject, object> toForeign;
			private Func<object, XmasObject> toXmas;

			public SleekConverter(Func<XmasObject, object> toForeign, Func<object, XmasObject> toXmas)
			{
				this.toForeign = toForeign;
				this.toXmas = toXmas;
			}

			internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
			{
				return toForeign(gobj);
			}

			internal override XmasObject BeginUnsafeConversionToXmas(object obj)
			{
				return toXmas(obj);
			}
		}
	}
}