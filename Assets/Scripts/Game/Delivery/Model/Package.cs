using GameObjects;
using UnityEngine;
using Zenject;

namespace Delivery
{
    public class Package : MonoBehaviour
    {
        private GameObject targetObject;

        public GameObject SpawnPoint { get; set; }

        public GameObject MinimapGameObject { get; set; }
        public GameObject TargetMinimapGameObject { get; set; }

        public int Price { set; get; }

        private DeliveryStatus status = DeliveryStatus.UNASSIGNED;

        public GameCharacter Biker { set; get; }

        public DeliveryStatus Status
        {
            set
            {
                status = value;
                HandleStatusChanged();
            }

            get => status;
        }

        public string Name { get; set; }

        public GameObject Target
        {
            set => targetObject = value;
            get => targetObject;
        }

        public void DestroyPackage()
        {
            Destroy(gameObject);
            Destroy(Target);
            Destroy(MinimapGameObject);
            Destroy(TargetMinimapGameObject);
        }

        private void HandleStatusChanged()
        {
            switch (status)
            {
                case DeliveryStatus.ASSIGNED:
                    MinimapGameObject.SetActive(false);
                    break;
                case DeliveryStatus.UNASSIGNED:
                    MinimapGameObject.SetActive(true);
                    break;
            }
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, Package>
        {
        }
    }
}
