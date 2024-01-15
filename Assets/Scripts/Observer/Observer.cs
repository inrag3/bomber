using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnUpdate();
}
public abstract class Observer<T> : MonoBehaviour
{
    public abstract void OnUpdate(T value);
}