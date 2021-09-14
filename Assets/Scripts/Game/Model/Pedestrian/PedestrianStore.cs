using System.Collections.Generic;

namespace Model
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
