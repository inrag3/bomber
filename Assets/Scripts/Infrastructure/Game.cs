using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public static ILevelService LevelService;
        public Game()
        {
            RegisterInputService();
            RegisterLevelService();
        }

        private static void RegisterLevelService() => 
            LevelService = new LevelService();

        private static void RegisterInputService()
        {
            if (!Application.isMobilePlatform)
                InputService = new StandaloneInputService();
        }
    }
}