using Enemies;
using Pedestrians;
using System.Collections.Generic;

namespace GameObjects
{
    public class CharacterStore
    {
        private PlayerStore playerStore;
        private EnemyStore enemyStore;
        private PedestrianStore pedestrianStore;

        public CharacterStore(PlayerStore playerStore, EnemyStore enemyStore, PedestrianStore pedestrianStore)
        {
            this.playerStore = playerStore;
            this.enemyStore = enemyStore;
            this.pedestrianStore = pedestrianStore;
        }

        public List<GameCharacter> GetPlayers()
        {
            return playerStore.GetAll();
        }

        public List<GameCharacter> GetEnemies()
        {
            return enemyStore.GetAll();
        }

        public List<Pedestrian> GetPedestrians()
        {
            return pedestrianStore.GetAll();
        }

        public void SetActivePlayer(GameCharacter player)
        {
            playerStore.SetActivePlayer(player);
        }

        public GameCharacter GetActivePlayer()
        {
            return playerStore.GetActivePlayer();
        }

    }
}
