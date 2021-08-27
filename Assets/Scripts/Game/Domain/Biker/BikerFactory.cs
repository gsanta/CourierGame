using AI;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BikerFactory : MonoBehaviour, ItemFactory<CourierConfig, Courier>
{
    [SerializeField]
    private MinimapBiker minimapTemplate;
    private Courier.Factory instanceFactory;
    private BikerStore courierStore;

    [Inject]
    public void Construct(Courier.Factory instanceFactory, BikerStore courierStore)
    {
        this.instanceFactory = instanceFactory;
        this.courierStore = courierStore;
    }

    public Courier Create(CourierConfig config)
    {
        Courier newBiker = instanceFactory.Create(courierStore.BikerTemplate);
        newBiker.transform.position = config.spawnPoint.transform.position;
        newBiker.goals.Add(config.goal, 3);
        newBiker.SetName(config.name);
        newBiker.gameObject.SetActive(true);

        MinimapBiker newMinimapBiker = Instantiate(minimapTemplate, minimapTemplate.transform.parent);
        newMinimapBiker.Biker = newBiker;
        newMinimapBiker.gameObject.SetActive(true);

        return newBiker;
    }
}
