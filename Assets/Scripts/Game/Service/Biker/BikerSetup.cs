using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BikerSetup

{
    private readonly BikerSpawner courierSpawner;

    public BikerSetup(BikerSpawner courierSpawner)
    {
        this.courierSpawner = courierSpawner;
    }

    public void Setup()
    {
        this.courierSpawner.Spawn();
    }
}
