using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public Game()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            if (!Application.isMobilePlatform)
                InputService = new StandaloneInputService();
        }
    }
}