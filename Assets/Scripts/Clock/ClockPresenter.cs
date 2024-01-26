using System;
using TMPro;
using UnityEngine;

public class ClockPresenter : Observer<float>
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Clock _clock;

    private void OnEnable() =>
        _clock.AddObserver(this);

    private void OnDisable() => 
        _clock.RemoveObserver(this);

    public override void OnUpdate(float value)
    {
        var timeSpan = TimeSpan.FromSeconds(value);
        _text.text = $"Time: {timeSpan:mm\\:ss}";
    }
}