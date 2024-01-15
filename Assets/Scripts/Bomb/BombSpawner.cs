using System;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        var position = transform.position;
        var cell = Grid.Instance[position.x, position.z];
        Instantiate(_bomb, cell, Quaternion.identity);
    }
}