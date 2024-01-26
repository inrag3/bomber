using System;
using Pool;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour, IPoolable
{
    private ParticleSystem _explosion;
    private readonly Vector3 _offset = new(0, 0.5f,0);

    private void Awake()
    {
        _explosion = GetComponent<ParticleSystem>();
    }
    public bool IsPlaying => _explosion.isPlaying;

    public void Explode(Vector3 position)
    {
        transform.position = position + _offset; 
        _explosion.Play();
    }

    public void Reload()
    {
        
    }
}