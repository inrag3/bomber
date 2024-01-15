using System;
using UnityEngine;

namespace Infrastructure
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;;
                if (count <= 0)
                    return _instance = new GameObject($"{typeof(T)}").AddComponent<T>();
                if (count != 1)
                    throw new Exception($"{typeof(T)} has {count} instances");
                return _instance = instances[0];
            }
        }
    }
}