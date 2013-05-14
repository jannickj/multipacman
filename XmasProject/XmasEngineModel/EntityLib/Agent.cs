namespace XmasEngineModel.EntityLib
{
	public class Agent : Unit
	{
		private string name;

		public string Name
		{
			get { return name; }
			protected set { name = value; }
		}
	}
}