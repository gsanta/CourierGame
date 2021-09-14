using Model;
using UnityEngine;
using Zenject;
public class BikerService : MonoBehaviour
{

    private BikerStore bikerStore;


    [Inject]
    public void Construct(BikerStore courierStore)
    {
        this.bikerStore = courierStore;
    }

    public Biker FindPlayRole()
    {
        return bikerStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY);
    }

    public Biker FindPlayOrFollowRole()
    {
        return bikerStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY || courier.GetCurrentRole() == CurrentRole.FOLLOW);
    }
}