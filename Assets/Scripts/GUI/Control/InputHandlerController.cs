using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class InputHandlerController : MonoBehaviour
    {
        [SerializeField]
        List<string> keys = new List<string>();

        private InputHandler inputHandler;

        [Inject]
        public void Construct(InputHandler inputHandler)
        {
            this.inputHandler = inputHandler;
        }

        private void Start()
        {
            inputHandler.SetKeys(keys);
        }

        private void Update()
        {
            inputHandler.Update();
        }
    }
}
