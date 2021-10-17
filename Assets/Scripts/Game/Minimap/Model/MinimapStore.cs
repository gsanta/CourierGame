using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap
{
    public class MinimapStore : IClearableStore
    {
        private List<GameObject> visiblePackages = new List<GameObject>();
        private List<GameObject> visiblePackageTargets = new List<GameObject>();

        public List<GameObject> VisiblePackageTargets
        {
            set
            {
                visiblePackageTargets = value;
                StoreChanged?.Invoke(this, EventArgs.Empty);
            }

            get => visiblePackageTargets;
        }

        public List<GameObject> VisiblePackages
        {
            set
            {
                visiblePackages = value;
                StoreChanged?.Invoke(this, EventArgs.Empty);
            }

            get => visiblePackages;
        }

        public event EventHandler StoreChanged;

        public void Clear()
        {
            visiblePackages = new List<GameObject>();
            visiblePackageTargets = new List<GameObject>();
        }
    }
}
