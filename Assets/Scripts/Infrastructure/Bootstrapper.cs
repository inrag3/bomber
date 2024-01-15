using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _game = new Game();
        }
    }
}
