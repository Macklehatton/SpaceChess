using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameGridExtensions
{
    public static Vector3 CenterOffset(this GameGrid gameGrid)
    {
        Vector3 centerOffset = new Vector3(
                        gameGrid.cellSize.x / 2.0f,
                        gameGrid.cellSize.y / 2.0f,
                        gameGrid.cellSize.z / 2.0f);
        return centerOffset;
    }
}
