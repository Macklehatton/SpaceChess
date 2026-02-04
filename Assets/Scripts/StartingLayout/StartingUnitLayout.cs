using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class StartingUnitLayout
{
    [SerializeField]
    [ListDrawerSettings(CustomAddFunction = "AddStartPosition")]
    [HideReferenceObjectPicker]
    public List<EntityStartPosition> unitPositions;

    public StartingUnitLayout()
    {
        unitPositions = new List<EntityStartPosition>();
    }

    public void AddStartPosition()
    {
        EntityStartPosition unitPosition = new EntityStartPosition();
        unitPosition.type = typeof(CombatUnit);
        unitPositions.Add(unitPosition);
    }
}
