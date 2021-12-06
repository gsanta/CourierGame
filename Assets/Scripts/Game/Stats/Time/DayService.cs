using GameObjects;
using System;

namespace Stats
{
    public class DayService
    {
        private readonly PlayerStore bikerStore;

        public DayService(Timer timer, PlayerStore bikerStore)
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
                biker.Agent.AbortAction();
            });
        }
    }
}
