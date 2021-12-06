using Enemies;
using System.Collections.Generic;

namespace GameObjects
{
    public class CharacterStore
    {
        private PlayerStore playerStore;
        private EnemyStore enemyStore;

        public CharacterStore(PlayerStore playerStore, EnemyStore enemyStore)
        {
            this.playerStore = playerStore;
            this.enemyStore = enemyStore;
        }

        public List<GameCharacter> GetPlayers()
        {
            return playerStore.GetAll();
        }

        public List<GameCharacter> GetEnemies()
        {
            return enemyStore.GetAll();
        }
    }
}
