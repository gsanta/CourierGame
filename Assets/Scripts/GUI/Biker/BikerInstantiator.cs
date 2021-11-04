
using Bikers;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class BikerInstantiator : MonoBehaviour, IBikerInstantiator
    {
        private BikerStore bikerStore;

        [Inject]
        public void Construct(BikerStore bikerStore, BikerFactory bikerFactory)
        {
            this.bikerStore = bikerStore;
            bikerFactory.SetBikerInstantiator(this);
        }

        public Biker InstantiateBiker()
        {
            return Instantiate(bikerStore.GetBikerTemplate(), bikerStore.GetBikerContainer().transform);
        }

        public MinimapBiker InstantiateMinimapBiker()
        {
            var minimapBiker = bikerStore.GetMinimapBiker();
            return Instantiate(minimapBiker, minimapBiker.transform.parent);
        }
    }
}
