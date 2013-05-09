namespace XmasEngineModel.Rule
{
	public class Conclusion
	{
		private object tag;

		public Conclusion()
		{
		}

		public Conclusion(object tag)
		{
			this.tag = tag;
		}

		public object Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		public override string ToString()
		{
			return tag.ToString();
		}
	}
}