using Bikers;
using Enemies;
using Pedestrians;
using System.Collections.Generic;

namespace GameObjects
{
    public class CharacterStore
    {
        List<Player> players = new List<Player>();
        List<Pedestrian> pedestrians = new List<Pedestrian>();
        List<Enemy> enemies = new List<Enemy>();

        public List<Player> Players { get => players; set => players = value; }
        public List<Pedestrian> Pedestrians { get => pedestrians; set => pedestrians = value; }
        public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    }
}
