using Levels;
using React;
using Weapons;

namespace Data.Game
{
    public static class GlobalData
    {
        public static int WeaponSlotsCurrent;
        public static int WeaponSlotsMax;

        public static int LevelIndex;
        public static int LevelTotal;
        
        public static IWeapon CurrentWeapon;
        public static Level CurrentLevel;

        public static ReactiveProperty<float> ShotsLeft = new ReactiveProperty<float>();
    }
}