using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class Pool<T> where T : Component, IPoolable
    {
        private readonly Queue<T> _entities = new Queue<T>();
        private readonly T _prefab;
        private readonly Transform _parent;

        public Pool(T prefab, int count, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            for (var i = 0; i < count; i++)
            {
                Return(Preload());
            }
        }

        public T Get()
        {
            var entity = _entities.Count > 0 ? _entities.Dequeue() : Preload();
            entity.gameObject.SetActive(true);
            return entity;
        }

        private T Preload() => Object.Instantiate(_prefab, _parent);

        public void Return(T entity)
        {
            entity.gameObject.SetActive(false);
            entity.Reload();
            _entities.Enqueue(entity);
        }

        public void Reset()
        {
            foreach (var entity in _entities)
            {
                Object.Destroy(entity.gameObject);
            }
            _entities.Clear();
        }
    }
}
