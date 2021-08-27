using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DayService
    {
        private readonly BikerStore bikerStore;

        public DayService(Timer timer, BikerStore bikerStore)
        {
            this.bikerStore = bikerStore;
            timer.DayPassed += HandleDayPassed;
            timer.DayStarted += HandleDayStarted;
        }

        private void HandleDayStarted(object sender, EventArgs args)
        {
            bikerStore.GetAll().ForEach(biker => {
                biker.Paused = false;
            });
        }

        private void HandleDayPassed(object sender, EventArgs args)
        {
            bikerStore.GetAll().ForEach(biker => {
                biker.Paused = true;
                biker.AbortAction();
            });
        }
    }
}
