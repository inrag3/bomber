using System;
using System.Collections;
using Pool;
using UnityEngine;


public class EnemyFactory : MonoBehaviour, IObserver<Enemy>
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnRate;
    private Pool<Enemy> _pool;
    private WaitForSeconds _timeOut;
    private const int PRELOAD_COUNT = 5;
    
    private void Awake()
    {
        _pool = new Pool<Enemy>(_enemy, PRELOAD_COUNT, transform);
        _timeOut = new WaitForSeconds(_spawnRate);
    }

    private void Start()
    {
        StartCoroutine(Create());
    }
    
    private IEnumerator Create()
    {
        while (true)
        {
            Create(transform.position);
            yield return _timeOut;
        }
    }
    private void Create(Vector3 position)
    {
        var enemy = _pool.Get();
        enemy.transform.position = position;
        enemy.AddObserver(this);
    }
    public void OnUpdate(Enemy enemy)
    {
        _pool.Return(enemy);
        enemy.RemoveObserver(this);
    }
}
