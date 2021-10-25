
namespace Worlds
{
    public class WorldStateCreator
    {
        private readonly WorldStore worldStore;

        public WorldStateCreator(WorldStore worldStore)
        {
            this.worldStore = worldStore;
        }

        public void Init()
        {
            var world1 = new WorldState();
            world1.Name = WorldState.GenerateWorldName(1);
            worldStore.AddWorld(world1);

            var world2 = new WorldState();
            world2.Name = WorldState.GenerateWorldName(2);
            worldStore.AddWorld(world2);
        }
    }
}
