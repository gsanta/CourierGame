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
        }

        private void HandleDayPassed(object sender, EventArgs args)
        {
            bikerStore.GetAll()
        }
    }
}
