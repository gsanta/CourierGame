using GameObjects;
using Enemies;
using GamePlay;
using UI;
using UnityEngine;
using Worlds;
using Zenject;

namespace Attacks
{
    [RequireComponent(typeof(SphereCollider))]
    public class AttackRadius : MonoBehaviour
    {
        private CanvasStore canvasStore;
        private TurnManager turnManager;
        private WorldStore worldStore;

        [Inject]
        public void Construct(CanvasStore canvasStore, TurnManager turnManager, WorldStore worldStore)
        {
            this.canvasStore = canvasStore;
            this.turnManager = turnManager;
            this.worldStore = worldStore;
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.gameObject.name == "Attack Radius")
            //{
            //    var messagePanel = canvasStore.GetPanel<MessagePanel>(typeof(MessagePanel));
            //    messagePanel.gameObject.SetActive(true);
            //    turnManager.Pause();
            //    worldStore.BattleState = new BattleState(transform.parent.GetComponent<Player>(), other.transform.parent.GetComponent<Enemy>());
            //}
        }
    }
}
