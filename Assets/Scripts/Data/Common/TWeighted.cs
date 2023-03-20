namespace Data
{
    [System.Serializable]
    public class TWeighted<T>
    {
        public T Value;
        public float Weight = 1f;
    }
}