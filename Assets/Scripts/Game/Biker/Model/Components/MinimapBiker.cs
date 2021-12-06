using System;
using UnityEngine;

namespace GameObjects
{
    public class MinimapBiker : MonoBehaviour
    {
        private GameCharacter biker;

        public GameCharacter Biker { set => biker = value; }

        private void Update()
        {
            transform.position = new Vector3(biker.transform.position.x, 5, biker.transform.position.z);
            transform.rotation = biker.transform.rotation;
        }
    }
}
