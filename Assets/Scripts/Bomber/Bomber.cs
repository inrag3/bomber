using UnityEngine;

public class Bomber : MonoBehaviour, IDamageable
{
    [field: SerializeReference] public int Health { get; private set; }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            //Какие-то действия
        }
    }
}