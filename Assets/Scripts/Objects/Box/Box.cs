using System;
using UnityEngine;

namespace Objects.Block
{
    public class Box : MonoBehaviour, IObservable<Box>, ITile
    {
        private IHealth _health;
        private event Action<Box> BoxBroke;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _health = GetComponent<IHealth>();    
        }

        private void OnEnable() => 
            _health.Died += OnDied;

        private void OnDisable() => 
            _health.Died -= OnDied;

        private void OnDied()
        {
            Notify(this);
            RemoveAllObservers();
            Destroy(gameObject);
        }

        private void RemoveAllObservers() => 
            BoxBroke = null;

        public void AddObserver(IObserver<Box> observer) => 
            BoxBroke += observer.OnUpdate;

        public void RemoveObserver(IObserver<Box> observer) =>
            BoxBroke -= observer.OnUpdate;

        public void Notify(Box value)
        {
            BoxBroke?.Invoke(value);
        }
    }
}