
using Delivery;
using UI;

namespace Game
{
    public interface IDeliveryListItemInstantiator
    {
        IDeliveryListItem Instantiate(Package package);
    }
}
