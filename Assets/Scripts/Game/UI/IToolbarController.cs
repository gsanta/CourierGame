
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IToolbarController
    {
        T GetWidget<T>(Type type) where T : class;
        List<GameObject> GetAllWidgets();

        void UpdateButtonStates();
    }
}
