using System;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;
    private Grid _grid;

    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        Instantiate(_bomb, transform.position, Quaternion.identity);
    }
}