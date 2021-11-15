using GamePlay;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class InvokeHelper : MonoBehaviour, IInvokeHelper
    {
        private ITurns pedestrianTurns;
        private GameObjectStore gameObjectStore;

        [Inject]
        public void Construct([Inject(Id = "PedestrianTurns")] ITurns pedestrianTurns, GameObjectStore gameObjectStore)
        {
            this.pedestrianTurns = pedestrianTurns;
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
    }
}
