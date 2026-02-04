using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    [SerializeField]
    private GameGridConfig config;
    [SerializeField]
    private CombatUnits combatUnits;
    [SerializeField]
    private GameObject selectionPlanePrefab;

    private GameGrid gameGrid;


    private void Awake()
    {
        GameObject gridObject = new GameObject();
        GridComponent gridComponent = gridObject.AddComponent<GridComponent>();
        gridComponent.gameGrid = new GameGrid(config.gridDimensions, config.gridCellSize);
        gridObject.tag = "GameGrid";
        gameGrid = gridComponent.gameGrid;

        List<EntityStartPosition> unitPositions = config.startingLayout.startingUnitLayout.unitPositions;

        foreach (EntityStartPosition unitPosition in unitPositions)
        {
            CombatUnit combatUnit;
            for (int i = 0; i < combatUnits.entries.Count; i++)
            {
                if (combatUnits.entries[i].ID == unitPosition.ID)
                {
                    combatUnit = combatUnits.entries[i];
                    gameGrid[unitPosition.gridPosition].contents.Add(combatUnit);
                    break;
                }
            }
        }
        SpawnEntities();

        Mathf.RoundToInt(gameGrid.gridDimensions.y / 2.0f);
        GameObject selectionPlaneObject = Instantiate(selectionPlanePrefab);
        selectionPlaneObject.transform.position = Vector3.Scale(gameGrid.gridDimensions, gameGrid.cellSize) / 2.0f - gameGrid.CenterOffset() - new Vector3(0.0f, gameGrid.cellSize.y / 2.0f, 0.0f);
        selectionPlaneObject.transform.localScale = Vector3.Scale(gameGrid.gridDimensions, gameGrid.cellSize) / 10.0f;
        selectionPlaneObject.tag = "SelectionPlane";
        SelectionPlaneComponent selectionPlaneComponent = selectionPlaneObject.AddComponent<SelectionPlaneComponent>();
        SelectionPlane selectionPlane = new SelectionPlane();
        selectionPlaneComponent.selectionPlane = selectionPlane;
        selectionPlane.height = Mathf.RoundToInt(gameGrid.gridDimensions.y / 2.0f);
    }

    private void Update ()
    {
            
	}

    private void SpawnEntities()
    {
        for (int i = 0; i < gameGrid.gameCells.GetLength(0); i++)
        {
            for (int j = 0; j < gameGrid.gameCells.GetLength(1); j++)
            {
                for (int k = 0; k < gameGrid.gameCells.GetLength(2); k++)
                {
                    Vector3 position = new Vector3Int(i, j, k) * config.gridCellSize;
                    Quaternion rotation = Quaternion.identity;
                    foreach (IGameEntity entity in gameGrid.gameCells[i, j, k].contents)
                    {
                        GameObject worldInstance = Instantiate(entity.Prefab, position, rotation);
                    }
                }
            }
        }
    }
}
