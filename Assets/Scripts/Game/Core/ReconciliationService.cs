﻿
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ReconciliationService : MonoBehaviour
    {
        private Dictionary<IDirty, List<IReconcilable>> map = new Dictionary<IDirty, List<IReconcilable>>();

        public void Add(IDirty dirty, IReconcilable reconcilable)
        {
            if (!map.ContainsKey(dirty))
            {
                map.Add(dirty, new List<IReconcilable>());
            }

            map[dirty].Add(reconcilable);
        }

        private void ReconcileAll()
        {

            foreach (var item in map)
            {
                if (item.Key.IsDirty())
                {
                    item.Key.ClearDirty();
                    item.Value.ForEach(val =>
                    {
                        val.Reconcile();
                    });
                }
            }
        }

        private void Update()
        {
            ReconcileAll();
        }
    }
}