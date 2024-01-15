using System;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class Seed
{
    [field: SerializeField] public int Value { get; private set; }

    public Seed() => Generate();

    private void Generate()
    {
        var random = new Random();
        Value = random.Next();
    }
}