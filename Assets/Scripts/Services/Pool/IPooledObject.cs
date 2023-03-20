namespace Services.Pool
{
    public interface IPooledObject<T> where T : IPooledObject<T>
    {
        void Init(IPool<T> pool);
        T GetObject();
        void HideToCollect();
        
    }
}