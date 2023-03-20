namespace Statues
{
    public interface IStatuePiece
    {
        void Damage(DamageArgs args);
        void Break();
        void Push();
    }
}