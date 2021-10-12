
using Core;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class ReconciliationController : MonoBehaviour
    {
        private ReconciliationService reconciliationService;

        [Inject]
        public void Construct(ReconciliationService reconciliationService)
        {
            this.reconciliationService = reconciliationService;
        }

        private void Update()
        {
            reconciliationService.ReconcileAll();
        }
    }
}
