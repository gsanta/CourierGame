using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CourierStore : MonoBehaviour
{
    private List<ICourier> couriers = new List<ICourier>();

    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private Courier courierTemplate;
    public GameObject[] SpawnPoints
    {
        get => spawnPoints;
    }

    public Courier CourierTemplate
    {
        get => courierTemplate;
    }

    public void Add(ICourier courier)
    {
        couriers.Add(courier);
        TriggerCourierAdded(courier);
    }

    public List<ICourier> GetAll()
    {
        return couriers;
    }

    public event EventHandler<CourierAddedEventArgs> OnCourierAdded;

    private void TriggerCourierAdded(ICourier courier)
    {
        EventHandler<CourierAddedEventArgs> handler = OnCourierAdded;
        if (handler != null)
        {
            handler(this, new CourierAddedEventArgs(courier));
        }
    }
}

public class CourierAddedEventArgs : EventArgs
{
    private readonly ICourier courier;

    internal CourierAddedEventArgs(ICourier courier)
    {
        this.courier = courier;
    }
    public ICourier Courier { get => courier; }
}

