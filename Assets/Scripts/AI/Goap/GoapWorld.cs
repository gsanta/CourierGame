namespace AI
{
    public sealed class GoapWorld
	{
		private static readonly GoapWorld instance = new GoapWorld();
		private static AIStates world;

		static GoapWorld()
		{
			world = new AIStates();
		}

		private GoapWorld() { }

		public static GoapWorld Instance
        {
			get { return instance; }
        }

		public AIStates GetWorld()
        {
			return world;
        }
	}
}

