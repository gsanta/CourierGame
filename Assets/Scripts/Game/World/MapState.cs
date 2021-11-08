
namespace Worlds
{
    public class MapState : IMapState
    {
        private bool curfew = false;
        private string name;

        public bool Curfew { get => curfew; set => curfew = value; }
        public string Name { get => name; set => name = value; }
    
        public static string GenerateWorldName(int index)
        {
            return "Map" + index + "Scene";
        }
    }
}
