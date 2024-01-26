using System;
using Pool;
using UnityEngine;

public class BombPlacer : Observer<Bomb>
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private Exploder _exploder;
    private Pool<Bomb> _pool;
    private const int PRELOAD_COUNT = 5;

    private void Awake()
    {
        var dummy = new GameObject("Bombs");
        _pool = new Pool<Bomb>(_bomb, PRELOAD_COUNT, dummy.transform);
    }

    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        var position = transform.position;
        var cellPosition = Grid.Instance[position.x, position.z];
        var bomb = _pool.Get();
        bomb.AddObserver(this);
        bomb.AddObserver(_exploder);
        bomb.Place(cellPosition);
    }
    public override void OnUpdate(Bomb bomb) =>
        _pool.Return(bomb);
}
