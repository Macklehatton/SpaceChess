using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[CreateAssetMenu(menuName = "Game/Combat Units")]
public class CombatUnits : SerializedScriptableObject
{
    [SerializeField]
    [ListDrawerSettings(CustomAddFunction ="AddCombatUnit")]
    public List<CombatUnit> entries;

    [SerializeField]
    [HideInInspector]
    private int lastID;

    private void Awake()
    {
        if (entries == null)
        {
            entries = new List<CombatUnit>();
            lastID = -1;
        }
    }

    public void AddCombatUnit()
    {
        CombatUnit combatUnit = new CombatUnit();
        combatUnit.ID = lastID + 1;
        lastID += 1;
        entries.Add(combatUnit);
    }
}
