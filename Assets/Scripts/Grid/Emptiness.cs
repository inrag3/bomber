using UnityEngine;

public class Emptiness : ITile
{
    public Vector3 Position { get; }

    public Emptiness(Vector3 position)
    {
        Position = position;
    }
}