using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    [SerializeField]
    public Vector3Int gridDimensions;
    [SerializeField]
    public Vector3 cellSize;

    public GameCell[,,] gameCells;

    public GameGrid(Vector3Int gridDimensions, Vector3 cellSize)
    {
        gameCells = new GameCell[gridDimensions.x, gridDimensions.y, gridDimensions.z];
        for (int i = 0; i < gameCells.GetLength(0); i++)
        {
            for (int j = 0; j < gameCells.GetLength(1); j++)
            {
                for (int k = 0; k < gameCells.GetLength(2); k++)
                {
                    GameCell gameCell = new GameCell();
                    gameCell.position = new Vector3Int(i, j, k);

                    gameCells[i, j, k] = gameCell;
                }
            }
        }
        this.cellSize = cellSize;
        this.gridDimensions = gridDimensions;
    }

    public GameCell this[int x, int y, int z]
    {
        get
        {
            return gameCells[x, y, z];
        }
        set
        {
            gameCells[x, y, z] = value;
        }
    }

    public GameCell this[Vector3Int position]
    {
        get
        {
            return gameCells[position.x, position.y, position.z];
        }
        set
        {
            gameCells[position.x, position.y, position.z] = value;
        }
    }    
}
