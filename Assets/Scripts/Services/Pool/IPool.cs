namespace Services.Pool
{
    public interface IPool<T> where T : IPooledObject<T>
    {
        T GetItem();
        void Return(T target);
        void CollectAllBack();
        void Spawn();

    }
}