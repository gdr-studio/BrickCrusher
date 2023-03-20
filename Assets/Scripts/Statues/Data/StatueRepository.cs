using Data;
using UnityEngine;

namespace Statues
{
    [CreateAssetMenu(fileName = nameof(StatueRepository), menuName = "SO/" + nameof(StatueRepository))]
    public class StatueRepository : PrefabRepository<BlockStatue, StatueName>, IStatueRepository
    {
    }
}