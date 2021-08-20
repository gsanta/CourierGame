using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CourierListItem : MonoBehaviour
    {

        public TMP_Text courierButtonText;

        private ICourier courier;
        private CourierStore courierStore;

        public ICourier Courier { set => courier = value;  }

        public CourierStore CourierStore { set => courierStore = value; }

        public void HandleClick()
        {
            ICourier activeCourier = courierStore.GetAll().Find(courier => courier.IsActive());

            if (activeCourier != null && activeCourier != courier)
            {
                activeCourier.SetActive(false);
            }

            courier.SetActive(true);
        }
    }
}
