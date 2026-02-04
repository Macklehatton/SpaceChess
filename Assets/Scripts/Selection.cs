using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class Selection : MonoBehaviour
{
    [SerializeField]
    private GameGrid gameGrid;
    [SerializeField]
    private float selectionWireThickness;
    [SerializeField]
    private float selectedCellThickness;
    [SerializeField]
    private Material selectionWireMaterial;
    [SerializeField]
    private Material selectedCellMaterial;

    private SelectionPlane selectionPlane;
    private VectorLine selectionWireLine;
    private VectorLine selectedCellLine;
    private GameCell selectedCell;

    private void Start()
    {
        selectionPlane = GameObject.FindWithTag("SelectionPlane").GetComponent<SelectionPlaneComponent>().selectionPlane;
        gameGrid = GameObject.FindWithTag("GameGrid").GetComponent<GridComponent>().gameGrid;
        selectionWireLine = new VectorLine("SelectionWire", new List<Vector3>(24), selectionWireThickness);
        selectedCellLine = new VectorLine("SelectedCell", new List<Vector3>(24), selectedCellThickness);
        selectionWireLine.material = selectionWireMaterial;
        selectedCellLine.material = selectedCellMaterial;
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        DrawCursor(gameGrid);
    }

    private void HandleSelection(GameCell cell)
    {
        if (Input.GetButton("Fire1"))
        {
            selectedCell = cell;
        }
        if (selectedCell != null)
        {
            UpdateSelectedCell(Vector3.Scale(selectedCell.position, gameGrid.cellSize));
        }
    }

    private void DrawCursor(GameGrid gameGrid)
    {
        float closest = Mathf.Infinity;
        Vector3 closestPosition = Vector3.zero;
        GameCell closestCell = null;

        for (int i = 0; i < gameGrid.gameCells.GetLength(0); i++)
        {
            for (int k = 0; k < gameGrid.gameCells.GetLength(2); k++)
            {                
                Vector3 worldPosition = new Vector3(i * gameGrid.cellSize.x, selectionPlane.height * gameGrid.cellSize.y, k * gameGrid.cellSize.z);
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

                float distance = Vector2.Distance(screenPosition, Input.mousePosition);
                if (distance < closest)
                {
                    closest = distance;
                    closestPosition = worldPosition;
                    closestCell = gameGrid[new Vector3Int(i, selectionPlane.height, k)];
                }
            }
        }
        UpdateSelectionWire(closestPosition);
        HandleSelection(closestCell);
    }

    private void UpdateSelectionWire(Vector3 position)
    {
        selectionWireLine.MakeCube(position, gameGrid.cellSize.x, gameGrid.cellSize.y, gameGrid.cellSize.z);
        selectionWireLine.Draw();        
    }

    private void UpdateSelectedCell(Vector3 position)
    {
        selectedCellLine.MakeCube(position, gameGrid.cellSize.x, gameGrid.cellSize.y, gameGrid.cellSize.z);
        selectedCellLine.Draw();
    }
}
