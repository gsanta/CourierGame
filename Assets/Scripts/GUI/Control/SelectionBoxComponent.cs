﻿using UnityEngine;
using Zenject;

namespace Controls
{
    public class SelectionBoxComponent : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;
        private SelectionBox selectionBox;

        [Inject]
        public void Construct(SelectionBox selectionBox)
        {
            this.selectionBox = selectionBox;
        }

        private void Awake()
        {
            selectionBox.SetRectTransform(rectTransform);
        }
    }
}
