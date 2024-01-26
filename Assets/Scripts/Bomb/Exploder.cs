using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class Exploder : MonoBehaviour, IObserver<Bomb>
{
    [SerializeField] private int _damage = 50;
    [SerializeField] private int _range = 3;
    [SerializeField] private Explosion _explosion;
    private Grid _grid;
    private readonly Collider[] _colliders = new Collider[25];
    private const int PRELOAD_COUNT = 5;
    private Pool<Explosion> _pool;
    
    // private IEnumerable<(ITile, ITile)> _bounds = new List<(ITile, ITile)>();

    private void Awake()
    {
        _grid = Grid.Instance;
        var dummy = new GameObject("Explosions");
        _pool = new Pool<Explosion>(_explosion, PRELOAD_COUNT, dummy.transform);
    }

    public void OnUpdate(Bomb bomb)
    {
        var position = bomb.transform.position;

        var bounds = _grid.GetCrossBounds(position, _range);
        foreach (var (x1, x2) in bounds)
        {
            var center = (x1.Position + x2.Position) / 2;
            var halfExtends = HalfExtends(x1.Position, x2.Position);
            var size = Physics.OverlapBoxNonAlloc(center, halfExtends, _colliders);
            print(size);
            for (var i = 0; i < size; i++)
            {
                if (!_colliders[i].TryGetComponent(out IHealth health))
                    continue;
                health.TakeDamage(_damage);
            }
        }
        StartCoroutine(Explode(position));
    }

    private IEnumerator Explode(Vector3 position)
    {
        var explosion = _pool.Get();
        explosion.gameObject.SetActive(true);
        explosion.Explode(position);
        yield return new WaitWhile(() => explosion.IsPlaying);
        _pool.Return(explosion);
    }

    private static Vector3 HalfExtends(Vector3 v1, Vector3 v2)
    {
        var deltaX = Mathf.Abs(v1.x - v2.x);
        var deltaY = Mathf.Abs(v1.y - v2.y);
        var deltaZ = Mathf.Abs(v1.z - v2.z);
        return new Vector3(deltaX / 2.0f, deltaY / 2.0f, deltaZ / 2.0f);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //
    //     foreach (var (x1, x2) in _bounds)
    //     {
    //         Gizmos.DrawCube(x1.Position, Vector3.one / 2f);
    //         Gizmos.DrawCube(x2.Position, Vector3.one / 2f);
    //     }
    // }
}