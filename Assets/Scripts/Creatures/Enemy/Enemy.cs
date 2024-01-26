using System;
using System.Collections;
using Creatures;
using Infrastructure;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using IPoolable = Pool.IPoolable;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Creature, IPoolable, IObservable<Enemy>
{
    private ITransformable _target;
    private NavMeshAgent _agent;
    private event Action<Enemy> Died;
    
    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        _target = FindObjectOfType<Bomber>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SetTarget(_target);
        }
    }
    
    
    protected override void OnDied()
    {
        Notify(this);
    }

    public void Reload()
    {
        transform.localPosition = Vector3.zero;
        Health.Heal(30);
    }

    private void SetTarget(ITransformable target)
    {
        if (target == null)
            return;
        _agent.SetDestination(target.Transform.position);
    }

    public void AddObserver(IObserver<Enemy> observer) => 
        Died += observer.OnUpdate;

    public void RemoveObserver(IObserver<Enemy> observer) => 
        Died -= observer.OnUpdate;

    public void Notify(Enemy enemy) =>
        Died?.Invoke(this);
}