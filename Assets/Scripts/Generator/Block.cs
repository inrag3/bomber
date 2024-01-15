
using System;
using Infrastructure;
using UnityEngine;

public class Block : MonoBehaviour, IDamageable, IObservable
{
    [field: SerializeReference] public int Health { get; private set; }
    private Action _blockBroke;
    
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health >= 0) 
            return;
        Notify();
        RemoveAllListeners();
        Destroy(gameObject);
    }

    private void RemoveAllListeners() => 
        _blockBroke = null;

    public void AddObserver(Observer observer)
    {
        _blockBroke += observer.OnUpdate;
    }

    public void RemoveObserver(Observer observer)
    {
        _blockBroke -= observer.OnUpdate;
    }

    public void Notify() => _blockBroke?.Invoke();
}