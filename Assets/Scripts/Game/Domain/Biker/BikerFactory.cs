using AI;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BikerFactory : MonoBehaviour, ItemFactory<CourierConfig, Biker>
{
    [SerializeField]
    private MinimapBiker minimapTemplate;
    private Biker.Factory instanceFactory;
    private BikerStore courierStore;

    [Inject]
    public void Construct(Biker.Factory instanceFactory, BikerStore courierStore)
    {
        this.instanceFactory = instanceFactory;
        this.courierStore = courierStore;
    }

    public Biker Create(CourierConfig config)
    {
        Biker newBiker = instanceFactory.Create(courierStore.BikerTemplate);
        newBiker.transform.position = config.spawnPoint.transform.position;
        newBiker.SetName(config.name);
        newBiker.gameObject.SetActive(true);

        MinimapBiker newMinimapBiker = Instantiate(minimapTemplate, minimapTemplate.transform.parent);
        newMinimapBiker.Biker = newBiker;
        newMinimapBiker.gameObject.SetActive(true);

        return newBiker;
    }
}
