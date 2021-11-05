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
    }
}
