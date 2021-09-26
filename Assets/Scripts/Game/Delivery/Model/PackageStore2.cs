
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class PackageStore2 : MonoBehaviour
    {
        [SerializeField]
        private List<Package> packages = new List<Package>();

        public List<Package> Packages { get => packages; }
    }
}
