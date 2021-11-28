using Bikers;
using UnityEngine;
using Worlds;
using Zenject;

namespace GamePlay
{
    public class BuildingSceneSetup : MonoBehaviour, ISceneEntryPoint
    {
        [SerializeField]
        GameObject playerPosition;
        PlayerStore playerStore;
        private WorldStore worldStore;

        [Inject]
        public void Construct(PlayerStore playerStore, WorldStore worldStore)
        {
            this.playerStore = playerStore;
            this.worldStore = worldStore;
        }

        public void Exit()
        {
            worldStore.CurrentMap = "Map1";
            var player = playerStore.GetActivePlayer();
            player.Agent.Active = true;
            player.transform.position = new Vector3(0, player.transform.position.y, 0);
        }

        public void Enter()
        {
            worldStore.CurrentMap = "Building";
            worldStore.ActiveSceneEntryPoint = this;
            var player = playerStore.GetActivePlayer();
            player.Agent.Active = true;
            var newPos = playerPosition.transform.position;
            player.transform.position = new Vector3(newPos.x, player.transform.position.y, newPos.z);
        }
    }
}
