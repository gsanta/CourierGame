using System;
using UnityEngine;

namespace Bikers
{
    public class MinimapBiker : MonoBehaviour
    {
        private Player biker;

        public Player Biker { set => biker = value; }

        private void Update()
        {
            transform.position = new Vector3(biker.transform.position.x, 5, biker.transform.position.z);
            transform.rotation = biker.transform.rotation;
        }
    }
}
