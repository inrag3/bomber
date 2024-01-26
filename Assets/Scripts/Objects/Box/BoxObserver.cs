namespace Objects.Block
{
    public class BoxObserver : Observer<Box>
    {
        private Grid _grid;

        private void Awake()
        {
            _grid = Grid.Instance;
        }
    
        public override void OnUpdate(Box box)
        {
            _grid.Remove(box);
        }
    }
}