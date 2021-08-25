﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PackageTarget : MonoBehaviour
{
    public void SetMeshVisibility(bool isVisible)
    {
       GetComponent<MeshRenderer>().enabled = isVisible;
    }

}