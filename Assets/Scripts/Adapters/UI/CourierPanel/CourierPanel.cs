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

        private CourierStore courierStore;

        [Inject]
        public void Construct(CourierStore courierStore)
        {
            this.courierStore = courierStore;
        }

        void Start()
        {
            courierStore.OnCourierAdded += HandleCourierAdded;
        }

        private void HandleCourierAdded(object sender, CourierAddedEventArgs args)
        {
            ICourier courier = args.Courier;

            CourierListItem courierListItem = Instantiate(courierListItemTemplate, courierListItemTemplate.transform.parent);
            courierListItem.courierButtonText.text = courier.GetName();
            courierListItem.gameObject.SetActive(true);
            courierListItem.CourierStore = courierStore;
            courierListItem.Courier = args.Courier;
            courierList.Add(courierListItem);
        }
    }
}
