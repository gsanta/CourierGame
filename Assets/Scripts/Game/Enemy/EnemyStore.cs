
using Scenes;
using System.Collections.Generic;

namespace Enemies
{
    public class EnemyStore : IResetable
    {
        private List<Enemy> enemies = new List<Enemy>();

        public void Add(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public List<Enemy> GetAll()
        {
            return enemies;
        }

        public void Reset()
        {
            enemies = new List<Enemy>();
        }
    }
}
