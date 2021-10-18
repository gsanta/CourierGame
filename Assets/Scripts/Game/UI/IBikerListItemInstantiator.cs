

using Bikers;

namespace UI
{
    public interface IBikerListItemInstantiator
    {
        public IBikerListItem Instantiate(Biker biker);
    }
}
