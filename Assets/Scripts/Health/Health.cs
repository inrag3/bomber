using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [field: SerializeField] public int Value { get; private set; }
    public event Action Died;

    public void TakeDamage(int amount)
    {
        Value -= amount;
        if (Value <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        Value += amount;
    }
    private void Die() => Died?.Invoke();
}