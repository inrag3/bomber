using System;
using System.Collections;
using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour, IObservable<float>
{
    [SerializeField] private float _time = 300f;
    private event Action<float> TimeChanged;
    
    private IEnumerator Start()
    {
        Notify(_time);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _time -= 1f;
            if (_time == 0)
            {
                Game.LevelService.Reload();
            }
            Notify(_time);
        }
    }
    public void AddObserver(IObserver<float> observer) => 
        TimeChanged += observer.OnUpdate;

    public void RemoveObserver(IObserver<float> observer) => 
        TimeChanged -= observer.OnUpdate;

    public void Notify(float time) =>
        TimeChanged?.Invoke(time);
}