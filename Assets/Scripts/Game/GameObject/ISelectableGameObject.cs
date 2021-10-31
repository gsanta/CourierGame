using Shaders;

namespace GameObjects
{
    public interface ISelectableGameObject
    {
        IGameObjectSelector GetGameObjectSelector();
    }
}
