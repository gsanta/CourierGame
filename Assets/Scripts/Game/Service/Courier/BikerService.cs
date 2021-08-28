using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class BikerService : MonoBehaviour
{

    private BikerStore courierStore;


    [Inject]
    public void Construct(BikerStore courierStore)
    {
        this.courierStore = courierStore;
    }

    public ICourier FindPlayRole()
    {
        return courierStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY);
    }

    public Biker FindPlayOrFollowRole()
    {
        return courierStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY || courier.GetCurrentRole() == CurrentRole.FOLLOW);
    }

    // TODO hide it from global space
    public void EmitCurrentRoleChanged()
    {
        CurrentRoleChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler CurrentRoleChanged;
}