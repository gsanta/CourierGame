using Scenes;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField]
        private string[] initialScenes;
        [SerializeField]
        private string[] mapScenes;
        private SceneLoader sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            sceneLoader.SetScenes(initialScenes, mapScenes);
        }
    }
}
