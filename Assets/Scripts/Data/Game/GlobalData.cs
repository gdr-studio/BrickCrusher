using Levels;
using React;
using Weapons;

namespace Data.Game
{
    public static class GlobalData
    {
        public static IWeapon CurrentWeapon;
        public static Level CurrentLevel;

        public static ReactiveProperty<float> ShotsLeft = new ReactiveProperty<float>();
    }
}