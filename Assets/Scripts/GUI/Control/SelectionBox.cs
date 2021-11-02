﻿using UnityEngine;
using Zenject;

namespace GUI
{
    public class SelectionBox : MonoBehaviour, ISelectionBox
    {
        public RectTransform selectionBox;
        private Vector2 startPos;
        private SelectionTool selectionTool;

        [Inject]
        public void Construct(SelectionTool selectionTool)
        {
            this.selectionTool = selectionTool;
        }

        private void Awake()
        {
            selectionTool.SetSelectionBox(this);
            selectionBox.gameObject.SetActive(false);
        }

        public void StartSelect()
        {
            startPos = Input.mousePosition;
            selectionBox.gameObject.SetActive(true);
        }

        public void UpdateSelect()
        {
            Vector2 currentMousePosition = Input.mousePosition;
            float width = currentMousePosition.x - startPos.x;
            float height = currentMousePosition.y - startPos.y;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
        }

        public SelectionRect EndSelect()
        {
            selectionBox.gameObject.SetActive(false);

            Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
            Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

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
