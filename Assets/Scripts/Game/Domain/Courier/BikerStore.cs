using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BikerStore : MonoBehaviour
{
    private List<Courier> bikers = new List<Courier>();

    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private Courier bikerTemplate;
    public GameObject[] SpawnPoints
    {
        get => spawnPoints;
    }

    public Courier BikerTemplate
    {
        get => bikerTemplate;
    }

    public void Add(Courier biker)
    {
        bikers.Add(biker);
        TriggerCourierAdded(biker);
    }

    public List<Courier> GetAll()
    {
        return bikers;
    }

    public event EventHandler<CourierAddedEventArgs> OnBikerAdded;

    private void TriggerCourierAdded(Courier biker)
    {
        EventHandler<CourierAddedEventArgs> handler = OnBikerAdded;
        if (handler != null)
        {
            handler(this, new CourierAddedEventArgs(biker));
        }
    }
}

public class CourierAddedEventArgs : EventArgs
{
    private readonly Courier courier;

    internal CourierAddedEventArgs(Courier courier)
    {
        this.courier = courier;
    }
    public Courier Courier { get => courier; }
}

