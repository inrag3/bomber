using System.Collections.Generic;
using Objects.Block;
using Objects.Wall;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Settings: ")] [SerializeField]
    private Point _leftTop;

    [SerializeField] [Min(1)] private int _width;
    [SerializeField] [Min(1)] private int _height;
    [SerializeField] [Range(0, 1)] private float _frequency;
    
    [Space] [Header("Tiles: ")]
    [SerializeField] private Tile _border;
    [SerializeField] private Tile _stone;
    [SerializeField] private Box _box;

    [Space] [Header("Ground Tile: ")] [SerializeField]
    private GameObject _grass;

    [SerializeField] private GameObject _sand;

    [Space] [SerializeField] private Grid _grid;
    [SerializeField] public Seed _seed;
    
    
    [SerializeField] private Observer<Box> _observer;
    [SerializeField] private EnemyFactory _factory;
    
    private void Awake()
    {
        _seed = new Seed();
        Generate();
    }

    private T Instantiate<T>(T prefab, Vector3 point, int i, int j, float offset) where T : Object
    {
        var position = GetPosition(point, i, j, offset);
        return Instantiate(prefab, position, Quaternion.identity, transform);
    }

    private Vector3 GetPosition(Vector3 point, int i, int j, float offset = 0)
    {
        var x = point.x + i * _grid.Size.x;
        var z = point.z - j * _grid.Size.y;
        return new Vector3(x, offset, z);
    }
    
    private void Generate()
    {
        //Расстановка неразрушаемых блоков.
        var point = _grid[_leftTop.X, _leftTop.Z];

        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                {
                    Instantiate(_sand, point, i, j, -2 * _grid.Offset);
                }
                else
                {
                    Instantiate(_grass, point, i, j, -2 * _grid.Offset);
                }
            }
        }
        
        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                if (i == 0 || j == 0 || i == _width - 1 || j == _height - 1)
                {
                    var border = Instantiate(_border, point, i, j, _grid.Offset);
                    _grid.Place(border);
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    var rock = Instantiate(_stone, point, i, j, _grid.Offset);
                    _grid.Place(rock);
                }
            }
        }
        //Расстановка разрушаемых блоков.

        var clears = new HashSet<(int, int)>()
        {
            (1, 1),
            (1, 2),
            (2, 1),
            (_width - 2, _height / 2), //Для EnemyFactory
        };
         var factoryPosition = GetPosition(point, _width - 2, _height / 2);
         _factory.transform.position = factoryPosition;
         
        var random = new System.Random(_seed.Value);
        for (var i = 1; i < _width - 1; i++)
        {
            for (var j = 1; j < _height - 1; j++)
            {
                if (i % 2 == 0 && j % 2 == 0)
                    continue;

                if (clears.Contains((i, j)) || random.NextDouble() < 1 - _frequency)
                    continue;
                var block = Instantiate(_box, point, i, j, _grid.Offset);
                block.AddObserver(_observer);
                _grid.Place(block);
            }
        }
    }
}