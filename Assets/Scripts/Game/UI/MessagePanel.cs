using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text message;

        private CanvasStore canvasStore;

        [Inject]
        public void Construct(CanvasStore canvasStore)
        {
            this.canvasStore = canvasStore;
        }

        public void SetMessage(string message)
        {
            this.message.text = message;
        }

        public void TakeAction()
        {
            canvasStore.HidePanel(typeof(MessagePanel));
        }

        public void SkipAction()
        {
            canvasStore.HidePanel(typeof(MessagePanel));
        }
    }
}
