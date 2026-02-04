using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell
{
    public Vector3Int position;
    public List<IGameEntity> contents;

    public GameCell()
    {
        contents = new List<IGameEntity>();
    }
}
