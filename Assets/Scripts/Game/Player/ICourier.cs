﻿using System;
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
    void SetActive(bool isActive);
    bool IsActive();
    public void SetPlayer(bool isPlayer);
    public bool IsPlayer();
    void SetPackage(Package package);
    Package GetPackage();
    Transform GetTransform();
}
