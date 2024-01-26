using UnityEngine;

public interface IObserver
{
    public void OnUpdate();
}

public interface IObserver<T>
{
    public void OnUpdate(T value);
}


public abstract class Observer<T> : MonoBehaviour, IObserver<T>
{
    public abstract void OnUpdate(T value);
}

public abstract class Observer : MonoBehaviour, IObserver
{
    public abstract void OnUpdate();
}