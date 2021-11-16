using GamePlay;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class InvokeHelper : MonoBehaviour, IInvokeHelper
    {
        private ITurns pedestrianTurns;
        private ITurns enemyTurns;
        private GameObjectStore gameObjectStore;

        [Inject]
        public void Construct([Inject(Id = "PedestrianTurns")] ITurns pedestrianTurns, [Inject(Id = "EnemyTurns")] ITurns enemyTurns, GameObjectStore gameObjectStore)
        {
            this.pedestrianTurns = pedestrianTurns;
            this.enemyTurns = enemyTurns;
            this.gameObjectStore = gameObjectStore;
        }

        private void Awake()
        {
            gameObjectStore.InvokeHelper = this;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return this;
        }

        public void InvokePedestrianTurns()
        {
            (pedestrianTurns as PedestrianTurns).Invoke();
        }

        public void InvokeEnemyTurns()
        {
            (enemyTurns as EnemyTurns).Invoke();
        }
    }
}
