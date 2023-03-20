using Data;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = nameof(CannonRepository), menuName = "SO/" + nameof(CannonRepository))]
    public class CannonRepository : PrefabRepository<Cannon, CannonName>
    {
    }
}