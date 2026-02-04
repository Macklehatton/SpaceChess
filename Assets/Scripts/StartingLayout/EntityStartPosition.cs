using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[System.Serializable]
public class EntityStartPosition
{
    [SerializeField]
    public Type type;
    [SerializeField]
    public int ID;
    [SerializeField]
    public Vector3Int gridPosition;
}
