using System.Collections.Generic;
using UI;
using UnityEngine;
using Worlds;
using Zenject;

namespace GamePlay
{
    public class SceneManagerData : MonoBehaviour
    {
        [SerializeField]
        public string[] initialScenes;
        [SerializeField]
        public string[] mapScenes;
        [SerializeField]
        public string[] initialPanels;
        [SerializeField]
        public string[] otherScenes;

        public Dictionary<string, FakeSceneLoader> fakeScenes = new Dictionary<string, FakeSceneLoader>();

        private CanvasInitializer canvasInitializer;
        private WorldStore worldStore;

        public FakeSceneLoader[] FakeSceneLoaders { get => GetComponents<FakeSceneLoader>(); }

        [Inject]
        public void Construct(SceneManager sceneManager, CanvasInitializer canvasInitializer, WorldStore worldStore)
        {
            this.canvasInitializer = canvasInitializer;
            this.worldStore = worldStore;
            sceneManager.Data = this;
        }

        private void Awake()
        {
            canvasInitializer.SetInitialPanels(initialPanels);

            foreach (var item in FakeSceneLoaders)
            {
                fakeScenes.Add($"{item.sceneName}Scene", item);
            }

            foreach (var mapScene in mapScenes)
            {
                var map = new MapState();
                map.Name = $"{mapScene}Scene";
                worldStore.AddWorld(map);
            }
        }
    }
}
