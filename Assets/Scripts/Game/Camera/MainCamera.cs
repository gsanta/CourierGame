﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    public void ResetPosition()
    {
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
    }
}