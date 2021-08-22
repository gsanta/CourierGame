using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ICourier
{
    GameObject GetGameObject();
    string GetId();
    string GetName();
    void SetName(string name);
    void SetPackage(Package package);
    Package GetPackage();
    Transform GetTransform();
    void SetCurrentRole(CurrentRole currentRole);
    CurrentRole GetCurrentRole();
}
