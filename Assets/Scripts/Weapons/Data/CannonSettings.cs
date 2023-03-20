using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = nameof(CannonSettings), menuName = "SO/" + nameof(CannonSettings))]
    public class CannonSettings : ScriptableObject
    {
        public ShootingSettings shooting;
        public MovingSettings moving;
    }
}