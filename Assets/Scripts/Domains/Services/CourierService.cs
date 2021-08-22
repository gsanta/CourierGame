using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class CourierService : MonoBehaviour
{

    private CourierStore courierStore;

    [Inject]
    public void Construct(CourierStore courierStore)
    {
        this.courierStore = courierStore;
    }

    public ICourier FindPlayRole()
    {
        return courierStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY);
    }

    public ICourier FindPlayOrFollowRole()
    {
        return courierStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY || courier.GetCurrentRole() == CurrentRole.FOLLOW);
    }
}
