using Bikers;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class BuildingSceneSetup : MonoBehaviour
    {
        [SerializeField]
        GameObject playerPosition;
        PlayerStore playerStore;

        [Inject]
        public void Construct(PlayerStore playerStore)
        {
            this.playerStore = playerStore;
        }

        private void Start()
        {
            var player = playerStore.GetActivePlayer();
            var newPos = playerPosition.transform.position;
            player.transform.position = new Vector3(newPos.x, player.transform.position.y, newPos.z);
        }
    }
}
