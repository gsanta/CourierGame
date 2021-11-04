using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Controls
{
    public class MouseController : MonoBehaviour
    {
        private Vector2 downPoint;
        private bool isDragging = false;

        private PointerHandler pointerHandler;

        [Inject]
        public void Construct(PointerHandler pointerHandler)
        {
            this.pointerHandler = pointerHandler;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                HandleMouseUp();
            } else if (Input.GetMouseButtonUp(1))
            {
                HandleRightButtonUp();
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                HandleLeftMouseDown();
            }

            if (Input.GetMouseButton(0))
            {
                HandleMouseMove();
            }
        }

        private void HandleMouseUp()
        {
            if (!isDragging)
            {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    pointerHandler.PointerClick();
                }
            } else
            {
                pointerHandler.PointerUp();
            }
        }

        private void HandleMouseMove()
        {
            Vector2 currPoint = Input.mousePosition;
            if (!isDragging && Vector2.Distance(downPoint, currPoint) > 5)
            {
                isDragging = true;
            }
            if (isDragging)
            {
                pointerHandler.PointerDrag();
            }
        }

        private void HandleRightButtonUp()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                pointerHandler.PointerRightClick();
            }
        }

        private void HandleLeftMouseDown()
        {
            isDragging = false;
            downPoint = Input.mousePosition;
            pointerHandler.PointerDown();
        }
    }
}
