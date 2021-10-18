
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IWidgetController
    {
        T GetWidget<T>(Type type) where T : class;
        List<GameObject> GetAllWidgets();
    }
}
