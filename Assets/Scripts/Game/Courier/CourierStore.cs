using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CourierStore
{
    private List<ICourier> couriers = new List<ICourier>();

    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private CourierAgent courierTemplate;
    public GameObject[] SpawnPoints
    {
        get => spawnPoints;
    }

    public CourierAgent CourierTemplate
    {
        get => courierTemplate;
    }

    public void Add(ICourier courier)
    {
        couriers.Add(courier);
    }

    public List<ICourier> GetAll()
    {
        return couriers;
    }
}
