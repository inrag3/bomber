using System;
using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour, IObservable<float>
{
    private Action<float> _timeChanged;

    private float _time;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _time += 1f;
            Notify();
        }
    }

    public void AddObserver(Observer<float> observer)
    {
        _timeChanged += observer.OnUpdate;
    }

    public void RemoveObserver(Observer<float> observer)
    {
        _timeChanged -= observer.OnUpdate;
    }

    public void Notify()
    {
        _timeChanged?.Invoke(_time);
    }
}