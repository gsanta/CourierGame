using Bikers;
using UnityEngine;
using Worlds;
using Zenject;

namespace GamePlay
{
    public class BuildingSceneSetup : MonoBehaviour
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

        private void Start()
        {
            worldStore.CurrentMap = "Building";
            var player = playerStore.GetActivePlayer();
            player.Agent.Active = true;
            var newPos = playerPosition.transform.position;
            player.transform.position = new Vector3(newPos.x, player.transform.position.y, newPos.z);
        }
    }
}
