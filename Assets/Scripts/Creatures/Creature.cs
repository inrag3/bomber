using UnityEngine;

namespace Creatures
{
    public abstract class Creature : MonoBehaviour, ICreature
    {
        public Transform Transform => transform;
        public IHealth Health { get; private set; }

        protected virtual void Awake()
        {
            Health = GetComponent<IHealth>();
        }

        private void OnEnable() =>
            Health.Died += OnDied;

        private void OnDisable() =>
            Health.Died -= OnDied;

        protected abstract void OnDied();
    }

    public interface ICreature : ITransformable
    {
        public IHealth Health { get; }
    }

    public interface ITransformable
    {
        public Transform Transform { get; }
    }
}