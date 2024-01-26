using System;
using System.Collections;
using Pool;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Bomb : MonoBehaviour, IObservable<Bomb>, IPoolable
{
    [SerializeField] private float _lifeTime = 3f;
    private event Action<Bomb> Exploded;
    private const string Bomber = "Bomber";
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public void Place(Vector3 position)
    {
        transform.position = position;
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(_lifeTime);
        Notify(this);
    }

    public void Reload()
    {
        _collider.isTrigger = true;
        RemoveAllObservers();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Bomber))
        {
            _collider.isTrigger = false;
        }
    }

    private void RemoveAllObservers() => 
        Exploded = null;
    public void AddObserver(IObserver<Bomb> observer) => 
        Exploded += observer.OnUpdate;

    public void RemoveObserver(IObserver<Bomb> observer) => 
        Exploded -= observer.OnUpdate;

    public void Notify(Bomb bomb) =>
        Exploded?.Invoke(bomb);
}