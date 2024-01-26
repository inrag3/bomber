namespace Infrastructure
{
    public interface ILevelService
    {
        public void Load(int index);
        public void Reload();
    }
}