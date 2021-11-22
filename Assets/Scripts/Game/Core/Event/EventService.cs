using Bikers;
using Delivery;
using Model;
using System;
using UnityEngine;

namespace Service
{
    public class EventService
    {
        public EventService()
        {
            Debug.Log("EventService created");
        }

        public void EmitPackageStatusChanged(Package package)
        {
            PackageStatusChanged?.Invoke(this, new PackageStatusChangedEventArgs(package));
        }

        public event EventHandler<PackageStatusChangedEventArgs> PackageStatusChanged;

        public void EmitBikerCurrentRoleChanged(Player biker)
        {
            BikerCurrentRoleChanged?.Invoke(this, new BikerCurrentRoleChangedEventArgs(biker));
        }
        public event EventHandler<BikerCurrentRoleChangedEventArgs> BikerCurrentRoleChanged;

        public void EmitMapSceneChanged()
        {
            MapSceneChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler MapSceneChanged;
    }
}
