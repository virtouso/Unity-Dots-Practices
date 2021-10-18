using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;


[GenerateAuthoringComponent]
public struct FloatData : IComponentData
{
    public float Speed;
}
