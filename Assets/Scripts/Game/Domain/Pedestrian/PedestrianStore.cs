using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PedestrianStore
    {
        private List<Pedestrian> pedestrians = new List<Pedestrian>();

        public void Add(Pedestrian pedestrian)
        {
            pedestrians.Add(pedestrian);
        }

        public List<Pedestrian> GetAll()
        {
            return pedestrians;
        }
    }
}
