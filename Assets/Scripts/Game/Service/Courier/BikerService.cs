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

    public Biker FindPlayRole()
    {
        return courierStore.GetAll().Find(courier => courier.GetCurrentRole() == CurrentRole.PLAY);
    }

    public void SetCurrentRole(CurrentRole currentRole, Biker biker)
    {
        var prevBiker = FindPlayOrFollowRole();

        if (prevBiker && prevBiker != biker && (currentRole == CurrentRole.PLAY || currentRole == CurrentRole.FOLLOW))
        {
            prevBiker.SetCurrentRole(CurrentRole.NONE);
        }

        biker.SetCurrentRole(currentRole);
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