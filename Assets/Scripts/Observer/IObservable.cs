using System;
using UnityEngine;
public interface IObservable
{
    public void AddObserver(IObserver observer);
    public void RemoveObserver(IObserver observer);
    public void Notify();
}

public interface IObservable<T>
{
    public void AddObserver(IObserver<T> observer);
    public void RemoveObserver(IObserver<T> observer);
    public void Notify(T value);
}




