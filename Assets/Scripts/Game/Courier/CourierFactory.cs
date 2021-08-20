using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CourierFactory : MonoBehaviour, ItemFactory<CourierConfig, CourierAgent>
{
    private CourierAgent.Factory instanceFactory;
    private CourierStore courierStore;


    [Inject]
    public void Construct(CourierAgent.Factory instanceFactory, CourierStore courierStore)
    {
        this.instanceFactory = instanceFactory;
        this.courierStore = courierStore;
    }

    public CourierAgent Create(CourierConfig config)
    {
        CourierAgent newCourier = instanceFactory.Create(courierStore.CourierTemplate);
        newCourier.transform.position = config.spawnPoint.transform.position;
        newCourier.goals.Add(config.goal, 3);

        return newCourier;
    }
}
