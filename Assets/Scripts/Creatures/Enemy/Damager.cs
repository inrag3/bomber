using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;
    private const string Bomber = "Bomber";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Bomber) && other.TryGetComponent<IHealth>(out var health))
        {
            health.TakeDamage(_damage);
        }
    }
}
