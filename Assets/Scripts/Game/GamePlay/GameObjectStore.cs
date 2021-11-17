using UI;
using UnityEngine;

namespace GamePlay
{
    public class GameObjectStore
    {
        public IInvokeHelper InvokeHelper { get; set; }
        
        public IBikerListItemInstantiator BikerListItemInstantiator { get; set; }
    }
}
