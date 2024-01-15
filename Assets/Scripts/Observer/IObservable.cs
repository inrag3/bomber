public interface IObservable
{
    public void AddObserver(Observer observer);
    public void RemoveObserver(Observer observer);
    public void Notify();
}

public interface IObservable<T>
{
    public void AddObserver(Observer<T> observer);
    public void RemoveObserver(Observer<T> observer);
    public void Notify();
}
