using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GUI
{
    public class MouseController : MonoBehaviour
    {
        private float downClickTime;
        private float ClickDeltaTime = 0.5f;

        private PointerHandler pointerHandler;

        [Inject]
        public void Construct(PointerHandler pointerHandler)
        {
            this.pointerHandler = pointerHandler;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseDown();
            }

            if (Input.GetMouseButtonUp(0))
            {
                HandleMouseUp();
            }

            if (Input.GetMouseButton(0))
            {
                HandleMouseDrag();
            }
        }

        private void HandleMouseUp()
        {
            if (Time.time - downClickTime <= ClickDeltaTime)
            {
                pointerHandler.PointerClick();
            } else
            {
                pointerHandler.PointerUp();
            }
        }

        private void HandleMouseDrag()
        {
            if (Time.time - downClickTime >= ClickDeltaTime)
            {
                pointerHandler.PointerDrag();
            }
        }

        private void HandleMouseDown()
        {
            downClickTime = Time.time;
            pointerHandler.PointerDown();
        }
    }
}
