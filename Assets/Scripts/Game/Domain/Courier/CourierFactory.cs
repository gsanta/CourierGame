using AI;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CourierFactory : MonoBehaviour, ItemFactory<CourierConfig, Courier>
{
    private Courier.Factory instanceFactory;
    private CourierStore courierStore;

    [Inject]
    public void Construct(Courier.Factory instanceFactory, CourierStore courierStore)
    {
        this.instanceFactory = instanceFactory;
        this.courierStore = courierStore;
    }

    public Courier Create(CourierConfig config)
    {
        Courier newCourier = instanceFactory.Create(courierStore.CourierTemplate);
        newCourier.transform.position = config.spawnPoint.transform.position;
        newCourier.goals.Add(config.goal, 3);
        newCourier.SetName(config.name);

        return newCourier;
    }
}
