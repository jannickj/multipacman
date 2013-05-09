using XmasEngineModel;

namespace XmasEngineView
{
	public class XmasWorldView
	{
		private XmasWorld model;

		public XmasWorldView(XmasWorld world)
		{
			this.model = world;
		}

		public XmasWorld Model
		{
			get { return model; }
		}
	}
}