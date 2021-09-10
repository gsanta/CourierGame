using AI;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BikerFactory : MonoBehaviour, ItemFactory<BikerConfig, Biker>
{
    [SerializeField]
    private MinimapBiker minimapTemplate;
    private Biker.Factory instanceFactory;
    private BikerStore bikerStore;

    [Inject]
    public void Construct(Biker.Factory instanceFactory, BikerStore bikerStore)
    {
        this.instanceFactory = instanceFactory;
        this.bikerStore = bikerStore;
    }

    public Biker Create(BikerConfig config)
    {
        Biker newBiker = instanceFactory.Create(bikerStore.BikerTemplate);
        newBiker.transform.position = config.spawnPoint.transform.position;
        newBiker.SetName(config.name);
        newBiker.gameObject.SetActive(true);

        MinimapBiker newMinimapBiker = Instantiate(minimapTemplate, minimapTemplate.transform.parent);
        newMinimapBiker.Biker = newBiker;
        newMinimapBiker.gameObject.SetActive(true);

        return newBiker;
    }
}
