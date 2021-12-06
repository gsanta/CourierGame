
using GameObjects;
using Scenes;
using System.Collections.Generic;

namespace Enemies
{
    public class EnemyStore : IResetable
    {
        private List<GameCharacter> enemies = new List<GameCharacter>();

        public void Add(GameCharacter enemy)
        {
            enemies.Add(enemy);
        }

        public List<GameCharacter> GetAll()
        {
            return enemies;
        }

        public void Reset()
        {
            enemies = new List<GameCharacter>();
        }
    }
}
