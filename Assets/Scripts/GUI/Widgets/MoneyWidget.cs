using Scenes;
using Stats;
using TMPro;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class MoneyWidget : MonoBehaviour, IReconcilable
    {
        [SerializeField]
        private TMP_Text money;

        private MoneyStore moneyStore;

        [Inject]
        public void Construct(MoneyStore moneyStore, ReconciliationService reconciliationService)
        {
            this.moneyStore = moneyStore;

            reconciliationService.Add(moneyStore, this);
        }

        public void Reconcile()
        {
            money.text = moneyStore.GetMoney().ToString() + "$";
        }
    }
}
