using UI;
using UnityEngine;
using Zenject;

namespace Attacks
{
    [RequireComponent(typeof(SphereCollider))]
    public class AttackRadius : MonoBehaviour
    {
        private CanvasStore canvasStore;

        [Inject]
        public void Construct(CanvasStore canvasStore)
        {
            this.canvasStore = canvasStore;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Attack Radius")
            {
                var messagePanel = canvasStore.GetPanel<MessagePanel>(typeof(MessagePanel));
                messagePanel.gameObject.SetActive(true);
            }
        }
    }
}
