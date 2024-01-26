using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class LevelService : ILevelService
    {
        public void Load(int index) => 
            SceneManager.LoadScene(index);

        public void Reload() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}