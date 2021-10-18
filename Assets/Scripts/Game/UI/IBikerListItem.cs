using Bikers;

namespace UI
{
    public interface IBikerListItem
    {
        void ResetToggleButtons(bool isFollow, bool isPlay);
        Biker GetBiker();
    }
}
