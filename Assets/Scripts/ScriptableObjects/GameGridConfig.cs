using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[CreateAssetMenu(menuName = "Game/GameGridConfig")]
public class GameGridConfig : SerializedScriptableObject
{
    [SerializeField]
    public Vector3Int gridDimensions;
    [SerializeField]
    public Vector3Int gridCellSize;

    [SerializeField]
    public BoundsInt playerOneStartingZone;
    [SerializeField]
    public BoundsInt playerTwoStartingZone;

    [SerializeField]
    [HideReferenceObjectPicker]
    public StartingEntityLayout startingLayout;

    private void Awake()
    {
        if (startingLayout == null)
        {
            startingLayout = new StartingEntityLayout();
        }
    }
}
