using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Block _block;
    [SerializeField] private Grid _grid;
    [SerializeField] public Seed _seed;
    [SerializeField] private Observer _observer;
    
    private void Awake()
    {
        Generate();
        //_seed = new Seed();
    }
    
    private void Generate()
    {
        /* Логика расстановки блоков
         *
         * 
         */
        
        var block = Instantiate(_block, Vector3.zero, Quaternion.identity);
        block.AddObserver(_observer);
    }
}