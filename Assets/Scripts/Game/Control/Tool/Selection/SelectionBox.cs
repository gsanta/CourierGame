using UnityEngine;

namespace Controls
{
    public struct SelectionRect
    {
        public readonly Vector2 min;
        public readonly Vector2 max;

        public SelectionRect(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public class SelectionBox
    {
        public RectTransform rectTransform;
        private Vector2 startPos;

        public void SetRectTransform(RectTransform rectTransform)
        {
            this.rectTransform = rectTransform;
            rectTransform.gameObject.SetActive(false);
        }

        public void StartSelect()
        {
            startPos = Input.mousePosition;
            rectTransform.gameObject.SetActive(true);
        }

        public void UpdateSelect()
        {
            Debug.Log("update select");
            Vector2 currentMousePosition = Input.mousePosition;
            float width = currentMousePosition.x - startPos.x;
            float height = currentMousePosition.y - startPos.y;
            rectTransform.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            rectTransform.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
        }

        public SelectionRect EndSelect()
        {
            rectTransform.gameObject.SetActive(false);

            Vector2 min = rectTransform.anchoredPosition - (rectTransform.sizeDelta / 2);
            Vector2 max = rectTransform.anchoredPosition + (rectTransform.sizeDelta / 2);

            return new SelectionRect(min, max);
        }

        //private void Update() 
        //{ 
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        return;
        //    }

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //    }

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        ReleaseSelectionBox();
        //    }

        //    if (Input.GetMouseButton(0))
        //    {
        //        UpdateSelectionBox(Input.mousePosition);
        //    }
        //}

        //private void UpdateSelectionBox(Vector2 currentMousePosition)
        //{
        //    if (!selectionBox.gameObject.activeInHierarchy)
        //    {
        //        selectionBox.gameObject.SetActive(true);
        //    }

        //    float width = currentMousePosition.x - startPos.x;
        //    float height = currentMousePosition.y - startPos.y;
        //    selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        //    selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
        //}

        //private void ReleaseSelectionBox()
        //{
        //    selectionBox.gameObject.SetActive(false);

        //    Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        //    Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        //    foreach(Pedestrian pedestrian in pedestrianStore.GetAll())
        //    {
        //        Vector3 screenPos = Camera.main.WorldToScreenPoint(pedestrian.transform.position);

        //        if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
        //        {
        //            pedestrian.GetGameObjectSelector().Deselect();
        //        }
        //    }
        //}
    }
}
