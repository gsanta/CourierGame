using GameObjects;
using Pedestrians;
using System.Collections.Generic;

namespace States
{
    public class SelectionStore
    {
        List<Pedestrian> pedestrians = new List<Pedestrian>();

        public void AddPedestrian(Pedestrian pedestrian)
        {
            pedestrians.Add(pedestrian);
        }

        public void Clear()
        {
            pedestrians.ForEach(pedestrian => pedestrian.GetGameObjectSelector().Deselect());
            pedestrians.Clear();
        }

        public List<Pedestrian> GetPedestrians()
        {
            return pedestrians;
        }
    }
}
