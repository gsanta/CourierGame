
using UnityEngine;
using UnityEngine.EventSystems;

namespace GUI
{
    public class PanHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Vector2 direction;

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}
