using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Service
{
    public class CourierCallbacksImpl : ICourierCallbacks
    {
        private readonly CourierService courierService;

        public CourierCallbacksImpl(CourierService courierService)
        {
            this.courierService = courierService;
        }

        public void OnCurrentRoleChanged(ICourier courier)
        {
            Debug.Log("Current role changed");
            courierService.EmitCurrentRoleChanged(courier);
        }
    }
}
