using System;

namespace XmasEngineModel.Exceptions
{
	public class UnconvertableException : Exception
	{
		private object gobj;


		public UnconvertableException(object gobj)
			: base("Conversion for object of type: " + gobj.GetType().Name + " Was not possible")
		{
			this.gobj = gobj;
		}

		public object ConvertingObject
		{
			get { return gobj; }
		}
	}
}