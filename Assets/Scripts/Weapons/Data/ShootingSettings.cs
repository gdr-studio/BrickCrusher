using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class ShootingSettings
    {
        public CannonBallName cannonBall;
        [Space(10)]
        public int MaxShots = 10;
        public float ShootDelay = 0.2f;
        [Space(10)]
        public int Damage = 1;
        public bool DamageOne = true;
        public float Range = 0;
        [Space(10)]
        public float MaxMoveDist = 10;
        public float MoveTime = 10;
    }
}