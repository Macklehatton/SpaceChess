using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class GridDrawing : MonoBehaviour
{
    [SerializeField]
    private Material planeLineMaterial;
    [SerializeField]
    private Material topBottomMaterial;
    [SerializeField]
    private Material tickMarkMaterial;
    [SerializeField]
    private Material grid3DMaterial;
    [SerializeField]
    private float lineThickness;
    [SerializeField]
    private float tickLineLength;

    public GameGrid gameGrid;

    private SelectionPlane selectionPlane;
    private List<VectorLine> planeGridLines;
    private List<VectorLine> bottomGridLines;
    private int lastPlaneHeight;

    private void Start()
    {
        lastPlaneHeight = 0;
        selectionPlane = GameObject.FindWithTag("SelectionPlane").GetComponent<SelectionPlaneComponent>().selectionPlane;
        gameGrid = GameObject.FindWithTag("GameGrid").GetComponent<GridComponent>().gameGrid;
        DrawTicks();
        DrawTopGrid();
        bottomGridLines = new List<VectorLine>();
        UpdateBottomGrid();
        planeGridLines = new List<VectorLine>();
        UpdatePlaneGrid();
    }

    private void Update()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        if (selectionPlane.height != lastPlaneHeight)
        {
            UpdatePlaneGrid();
            UpdateBottomGrid();
            lastPlaneHeight = selectionPlane.height;
        }
        DrawLines(planeGridLines);
        DrawLines(bottomGridLines);
    }

    private void DrawTicks()
    {
        List<VectorLine> vectorLines = new List<VectorLine>();

        for (int i = 0; i <= gameGrid.gameCells.GetLength(0); i++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, 0.0f, 0.0f) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, 0.0f, tickLineLength) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = tickMarkMaterial;
            vectorLines.Add(line);
        }

        for (int j = 0; j <= gameGrid.gameCells.GetLength(1); j++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(0.0f, j * gameGrid.cellSize.y, 0.0f) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(0.0f, j * gameGrid.cellSize.y, tickLineLength) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = tickMarkMaterial;
            vectorLines.Add(line);
        }

        for (int k = 0; k <= gameGrid.gameCells.GetLength(2); k++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(0.0f, 0.0f, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(tickLineLength, 0.0f, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = tickMarkMaterial;
            vectorLines.Add(line);
        }

        foreach (VectorLine line in vectorLines)
        {
            line.Draw3DAuto();
        }
    }

    private void UpdateBottomGrid()
    {
        VectorLine.Destroy(bottomGridLines);

        if (selectionPlane.height == 0)
        {
            return;
        }

        for (int i = 0; i <= gameGrid.gameCells.GetLength(0); i++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, 0.0f, 0.0f) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, 0.0f, gameGrid.gridDimensions.z * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = topBottomMaterial;
            bottomGridLines.Add(line);
        }

        for (int k = 0; k <= gameGrid.gameCells.GetLength(2); k++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(0.0f, 0.0f, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(gameGrid.gridDimensions.x * gameGrid.cellSize.x, 0.0f, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = topBottomMaterial;
            bottomGridLines.Add(line);
        }       
    }

    private void DrawTopGrid()
    {
        List<VectorLine> vectorLines = new List<VectorLine>();

        for (int i = 0; i <= gameGrid.gameCells.GetLength(0); i++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, (gameGrid.gridDimensions.y) * gameGrid.cellSize.y, 0.0f) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, (gameGrid.gridDimensions.y) * gameGrid.cellSize.y, gameGrid.gridDimensions.z * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = topBottomMaterial;
            vectorLines.Add(line);
        }

        for (int k = 0; k <= gameGrid.gameCells.GetLength(2); k++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(0.0f, (gameGrid.gridDimensions.y) * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(gameGrid.gridDimensions.x * gameGrid.cellSize.x, (gameGrid.gridDimensions.y) * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = topBottomMaterial;
            vectorLines.Add(line);
        }

        foreach (VectorLine line in vectorLines)
        {
            line.Draw3DAuto();
        }
    }

    private void UpdatePlaneGrid()
    {
        VectorLine.Destroy(planeGridLines);
        
        for (int i = 0; i <= gameGrid.gameCells.GetLength(0); i++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, selectionPlane.height * gameGrid.cellSize.y, 0.0f) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(i * gameGrid.cellSize.x, selectionPlane.height * gameGrid.cellSize.y, gameGrid.gridDimensions.z * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = planeLineMaterial;
            planeGridLines.Add(line);
        }

        for (int k = 0; k <= gameGrid.gameCells.GetLength(2); k++)
        {
            List<Vector3> linePoints = new List<Vector3>();
            linePoints.Add(new Vector3(0.0f, selectionPlane.height * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            linePoints.Add(new Vector3(gameGrid.gridDimensions.x * gameGrid.cellSize.x, selectionPlane.height * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
            VectorLine line = new VectorLine("Line", linePoints, lineThickness);
            line.material = planeLineMaterial;
            planeGridLines.Add(line);
        }

    }

    private void DrawLines(List<VectorLine> lines)
    {
        foreach (VectorLine line in lines)
        {
            line.Draw3D();
        }
    }

    private void Draw3DGrid()
    {
        List<VectorLine> vectorLines = new List<VectorLine>();

        for (int i = 0; i <= gameGrid.gameCells.GetLength(0); i++)
        {
            for (int j = 0; j <= gameGrid.gameCells.GetLength(1); j++)
            {
                List<Vector3> linePoints = new List<Vector3>();
                linePoints.Add(new Vector3(i * gameGrid.cellSize.x, j * gameGrid.cellSize.y, 0.0f) - gameGrid.CenterOffset());
                linePoints.Add(new Vector3(i * gameGrid.cellSize.x, j * gameGrid.cellSize.y, gameGrid.gridDimensions.z * gameGrid.cellSize.z) - gameGrid.CenterOffset());
                VectorLine line = new VectorLine("Line", linePoints, lineThickness, LineType.Points);
                line.material = grid3DMaterial;
                vectorLines.Add(line);
            }
        }

        for (int k = 0; k <= gameGrid.gameCells.GetLength(2); k++)
        {
            for (int j = 0; j <= gameGrid.gameCells.GetLength(1); j++)
            {
                List<Vector3> linePoints = new List<Vector3>();
                linePoints.Add(new Vector3(0.0f, j * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
                linePoints.Add(new Vector3(gameGrid.gridDimensions.x * gameGrid.cellSize.x, j * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
                VectorLine line = new VectorLine("Line", linePoints, lineThickness, LineType.Points);
                line.material = grid3DMaterial;
                vectorLines.Add(line);
            }
        }

        for (int i = 0; i <= gameGrid.gameCells.GetLength(0); i++)
        {
            for (int k = 0; k <= gameGrid.gameCells.GetLength(2); k++)
            {
                List<Vector3> linePoints = new List<Vector3>();
                linePoints.Add(new Vector3(i * gameGrid.cellSize.x, 0.0f, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
                linePoints.Add(new Vector3(i * gameGrid.cellSize.x, gameGrid.gridDimensions.y * gameGrid.cellSize.y, k * gameGrid.cellSize.z) - gameGrid.CenterOffset());
                VectorLine line = new VectorLine("Line", linePoints, lineThickness, LineType.Points);
                line.material = grid3DMaterial;
                vectorLines.Add(line);
            }
        }

        foreach (VectorLine line in vectorLines)
        {
            line.Draw3DAuto();
        }
    }

}
