using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CourierPanel : MonoBehaviour
    {
        [SerializeField]
        private CourierListItem courierListItemTemplate;
        private List<CourierListItem> courierList = new List<CourierListItem>();

        private BikerStore courierStore;
        private CourierService courierService;
        private MainCamera mainCamera;

        [Inject]
        public void Construct(BikerStore courierStore, CourierService courierService, MainCamera mainCamera)
        {
            this.courierStore = courierStore;
            this.courierService = courierService;
            this.mainCamera = mainCamera;
        }

        void Start()
        {
            courierStore.OnBikerAdded += HandleCourierAdded;
        }

        private void HandleCourierAdded(object sender, CourierAddedEventArgs args)
        {
            ICourier courier = args.Courier;

            CourierListItem courierListItem = Instantiate(courierListItemTemplate, courierListItemTemplate.transform.parent);
            courierListItem.courierNameText.text = courier.GetName();
            courierListItem.gameObject.SetActive(true);
            courierListItem.CourierService = courierService;
            courierListItem.Courier = args.Courier;
            courierListItem.MainCamera = mainCamera;
            courierList.Add(courierListItem);
        }

        public void ResetListItemsToggleButtons()
        {
            courierList.ForEach(item => item.ResetToggleButtons());
        }
    }
}
