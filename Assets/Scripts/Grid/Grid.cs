using System;
using System.Collections.Generic;
using Infrastructure;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Grid))]
public class Grid : Singleton<Grid>
{
    [SerializeField] private float _offset = 0.35f;
    private UnityEngine.Grid _grid;
    private readonly Dictionary<Vector3, ITile> _tiles = new();
    public Vector3 Size => _grid.cellSize;

    public float Offset => _offset;
    
    private void Awake()
    {
        _grid = GetComponent<UnityEngine.Grid>();
    }
    
    public Vector3 this[float x, float y] =>
        _grid.GetCellCenterWorld(_grid.WorldToCell(new Vector3(x, 0, y)));

    public void Place(ITile tile) =>
        _tiles.Add(tile.Position, tile);

    public void Remove(ITile tile) =>
        _tiles.Remove(tile.Position);

    public IEnumerable<(ITile, ITile)> GetCrossBounds(Vector3 position, int range)
    {
        position += Vector3.up * _offset;
        var x1 = FindTile(position, range,  Vector3.left);
        var x2 = FindTile(position, range,  Vector3.right);
        var y1 = FindTile(position, range,  Vector3.forward);
        var y2 = FindTile(position, range, Vector3.back);
        return new List<(ITile, ITile)>(2)
        {
            (x1, x2),
            (y1, y2)
        };
    }

    private ITile FindTile(Vector3 position, int range, Vector3 direction)
    {
        for (var i = 0; i < range; i++)
        {
            var tilePosition = position + direction * i;
            if (!_tiles.TryGetValue(tilePosition, out var tile)) 
                continue;
            return tile;
        }
        return new Emptiness( position + direction * range);
    }
}