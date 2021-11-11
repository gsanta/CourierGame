using UnityEngine;
using Zenject;

namespace Scenes
{
    public class TestsSceneInitializer : MonoBehaviour, ISceneInitializer
    {
        private SceneChangeHandler sceneChangeHandler;
        private ISceneSetup sceneSetup;

        [Inject]
        public void Construct(SceneChangeHandler sceneChangeHandler)
        {
            this.sceneChangeHandler = sceneChangeHandler;
        }

        public void SetSceneSetup(ISceneSetup sceneSetup)
        {
            this.sceneSetup = sceneSetup;
        }

        private void Awake()
        {
            sceneChangeHandler.SetSceneInitializer(this);
        }

        private void Start()
        {
            sceneSetup.SetupScene();
        }

        public void InitializeScene()
        {
        }
    }
}
