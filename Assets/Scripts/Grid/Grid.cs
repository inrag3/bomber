using System;
using Infrastructure;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Grid))]
public class Grid : Singleton<Grid>
{
    private UnityEngine.Grid _grid;
    public Vector2 Size => _grid.cellSize;

    private void Awake()
    {
        _grid = GetComponent<UnityEngine.Grid>();
    }

    public Vector3 this[float x, float y] => 
        _grid.GetCellCenterWorld(_grid.WorldToCell(new Vector3(x, 0, y)));
}