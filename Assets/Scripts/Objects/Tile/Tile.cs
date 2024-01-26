using UnityEngine;

namespace Objects.Wall
{
    public class Tile : MonoBehaviour, ITile
    {
        public Vector3 Position => transform.position;
    }
}