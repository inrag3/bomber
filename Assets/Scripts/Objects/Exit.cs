using System;
using Infrastructure;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bomber bomber))
        {
            Game.LevelService.Reload();
        }
    }
}
