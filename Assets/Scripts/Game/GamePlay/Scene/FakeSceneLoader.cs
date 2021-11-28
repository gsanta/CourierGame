using UnityEngine;

namespace GamePlay
{
    public class FakeSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private GameObject rootGameObject;
        [SerializeField]
        public string sceneName;

        private void Awake()
        {
            rootGameObject.SetActive(false);
        }

        public void Load()
        {
            rootGameObject.SetActive(true);
            rootGameObject.GetComponent<ISceneEntryPoint>().Enter();
        }

        public void Unload()
        {
            rootGameObject.GetComponent<ISceneEntryPoint>().Exit();
            rootGameObject.SetActive(false);    
        }
    }
}
