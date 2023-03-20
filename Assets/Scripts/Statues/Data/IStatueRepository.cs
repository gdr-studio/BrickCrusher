namespace Statues
{
    public interface IStatueRepository
    {
        BlockStatue GetPrefab(StatueName name);
    }
}