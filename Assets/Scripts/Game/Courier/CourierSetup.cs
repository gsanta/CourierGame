using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CourierSetup
{
    private readonly BikerSpawner courierSpawner;

    public CourierSetup(BikerSpawner courierSpawner)
    {
        this.courierSpawner = courierSpawner;
    }

    public void Setup()
    {
        this.courierSpawner.Spawn();
    }
}
