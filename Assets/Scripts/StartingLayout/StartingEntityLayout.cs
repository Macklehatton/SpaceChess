using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class StartingEntityLayout
{
    [OdinSerialize]
    [HideReferenceObjectPicker]
    public StartingUnitLayout startingUnitLayout;
    [OdinSerialize]
    [HideReferenceObjectPicker]
    public StartingObstacleLayout startingObstacleLayout;

    public StartingEntityLayout()
    {
        startingUnitLayout = new StartingUnitLayout();
        startingObstacleLayout = new StartingObstacleLayout();
    }
}
