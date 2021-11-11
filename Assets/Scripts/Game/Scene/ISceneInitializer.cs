

namespace Scenes
{
    public interface ISceneInitializer
    {
        void InitializeScene();
        void SetSceneSetup(ISceneSetup sceneSetup);
    }
}
