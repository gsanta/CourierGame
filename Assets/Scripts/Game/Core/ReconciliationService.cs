
using System.Collections.Generic;

namespace Core
{
    public class ReconciliationService
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

        public void ReconcileAll()
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
    }
}
