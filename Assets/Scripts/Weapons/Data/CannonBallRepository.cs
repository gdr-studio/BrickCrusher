using Data;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = nameof(CannonBallRepository), menuName = "SO/" + nameof(CannonBallRepository))]
    public class CannonBallRepository :  PrefabRepository<CannonBall, CannonBallName>
    {
    }
}