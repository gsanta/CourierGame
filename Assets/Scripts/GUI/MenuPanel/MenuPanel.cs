﻿using GamePlay;
using Scenes;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class MenuPanel : MonoBehaviour
    {
        private SceneManager sceneManager;

        [Inject]
        public void Construct(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public void OnLoadMap1()
        {
            this.sceneManager.UnLoadMapScene(1);
            this.sceneManager.LoadMapScene(0);
        }

        public void OnLoadMap2()
        {
            this.sceneManager.UnLoadMapScene(0);
            this.sceneManager.LoadMapScene(1);
        }
    }
}
