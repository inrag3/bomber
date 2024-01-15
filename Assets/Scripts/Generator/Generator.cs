using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    [Header("Settings: ")] [SerializeField]
    private Point _leftTop;
    
    [SerializeField] [Min(1)] private int _width;
    [SerializeField] [Min(1)] private int _height;
    [SerializeField] [Range(0, 1)]  private float _frequency;

    [Space] [Header("Materials: ")] [SerializeField]
    private GameObject _wall;

    [SerializeField] private Block _block;

    [Space] [SerializeField] private Grid _grid;
    [SerializeField] public Seed _seed;
    [SerializeField] private Observer _observer;

    private void Awake()
    {
        _seed = new Seed();
        Generate();
    }

    private void Generate()
    {
        //Расстановка неразрушаемых блоков.
        var point = _grid[_leftTop.X, _leftTop.Y];
        for (var i = 0; i < _width; i++)
        {
            for (var j = 0; j < _height; j++)
            {
                if ((i % 2 != 0 || j % 2 != 0) && i != 0 && i != _width - 1 && j != 0 && j != _height - 1)
                    continue;
                var x = point.x + i * _grid.Size.x;
                var y = point.z - j * _grid.Size.y;
                Instantiate(_wall, new Vector3(x, 0, y), Quaternion.identity, transform);
            }
        }
        
        //Расстановка разрушаемых блоков.
        
        var clears = new HashSet<(int, int)>()
        {
            (1, 1),
            (1, 2),
            (2, 1),
        };
        var random = new System.Random(_seed.Value);
        for (var i = 1; i < _width - 1; i++)
        {
            for (var j = 1; j < _height - 1; j++)
            {
                if (i % 2 == 0 && j % 2 == 0)
                    continue;

                if (clears.Contains((i, j)) || random.NextDouble() < 1 - _frequency)
                    continue;

                var x = point.x + i * _grid.Size.x;
                var y = point.z - j * _grid.Size.y;
                var block = Instantiate(_block, new Vector3(x, 0, y), Quaternion.identity, transform);
                block.AddObserver(_observer);
            }
        }
    }
}