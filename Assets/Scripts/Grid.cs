using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    int width;
    int height;
    float cellSize;
    Vector3 originPosition;
    TGridObject[,] gridArray;
    TextMesh[,] debugGridArray;

    bool showDebug = true;
        
    public Grid (int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        if (showDebug)
        {
            debugGridArray = new TextMesh[width, height];
    
            for(int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //debugGridArray[x, y] = CreateWorldText(gridArray[x, y]?.ToString(), GetCellPosition(x, y) + new Vector3 (cellSize, cellSize, 0) / 2, 15, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetCellPosition(x, y), GetCellPosition(x, y+1), Color.white, 100f);
                    Debug.DrawLine(GetCellPosition(x, y), GetCellPosition(x+1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetCellPosition(0, height), GetCellPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetCellPosition(width, 0), GetCellPosition(width, height), Color.white, 100f);
        }
    }

    public static TextMesh CreateWorldText (string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        return textMesh;
    }

    public Vector3 GetCellPosition(int x, int y)
    {
        return new Vector3 (x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    // public void AddValue (int x, int y, int value)
    // {
    //     if (x >= 0 && y >= 0 && x < width && y < height)
    //     {
    //         gridArray[x, y] += value;
    //         debugGridArray[x, y].text = gridArray[x, y].ToString();
    //     }
    // }

    // public void AddValue (Vector3 worldPosition, int value)
    // {
    //     int x, y;
    //     GetXY(worldPosition, out x, out y);
    //     AddValue(x, y, value);
    // }

    public void SetGridObject (int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (showDebug)
            {
                debugGridArray[x, y].text = gridArray[x, y]?.ToString();
            }
        }
    }

    public void SetGridObject (Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject (int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x < gridArray.GetLength(0) && y < gridArray.GetLength(1))
        {
            return GetGridObject(x, y);
        }
        else
        {
            return default(TGridObject);
        }
    }

    public int GetWidth()
    {
        return gridArray.GetLength(0);
    }

    public int GetHeight()
    {
        return gridArray.GetLength(1);
    }
}
