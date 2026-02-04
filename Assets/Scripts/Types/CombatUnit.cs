using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class CombatUnit : IGameEntity
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int ID;

    [ShowInInspector]
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
        }
    }
       
    [SerializeField]
    [HideInInspector]
    public GameObject prefab;
    
    [SerializeField]
    public int maxMovement;
    [SerializeField]
    public int maxRange;
    [SerializeField]
    public float attackDamage;
}
