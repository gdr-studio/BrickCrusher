using React;

namespace Money
{
    public static class MoneyCounter
    {
        public static ReactiveProperty<float> TotalMoney = new ReactiveProperty<float>();
        public static ReactiveProperty<float> LevelMoney = new ReactiveProperty<float>();

    }
}