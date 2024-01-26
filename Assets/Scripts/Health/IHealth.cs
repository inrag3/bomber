using System;

public interface IHealth
{
    public int Value { get; }
    public event Action Died;
    public void TakeDamage(int amount);

    public void Heal(int amount);
}