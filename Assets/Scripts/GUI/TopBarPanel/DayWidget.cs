using Core;
using Stats;
using TMPro;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class DayWidget : MonoBehaviour, IReconcilable
    {
        [SerializeField]
        private TMP_Text money;

        private Timer timer;

        [Inject]
        public void Construct(Timer timer, ReconciliationService reconciliationService)
        {
            this.timer = timer;

            reconciliationService.Add(timer, this);
        }

        public void Reconcile()
        {
            money.text = timer.CurrentDay.ToString();
        }

    }
}
