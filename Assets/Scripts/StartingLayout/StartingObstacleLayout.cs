using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class StartingObstacleLayout
{
    [SerializeField]
    [HideReferenceObjectPicker]
    public List<EntityStartPosition> obstaclePositions;

    public StartingObstacleLayout()
    {
        obstaclePositions = new List<EntityStartPosition>();
    }
}
