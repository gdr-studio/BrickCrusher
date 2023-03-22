using React;

namespace Money
{
    public static class MoneyCounter
    {
        public static ReactiveProperty<int> TotalMoney = new ReactiveProperty<int>();
        public static ReactiveProperty<int> LevelMoney = new ReactiveProperty<int>();

        public static void Clear()
        {
            TotalMoney.Val = 0;
            LevelMoney.Val = 0;
        }
        // public const string SavedMoneyKey = "MoneyAmount";
        // public static int SavedMoney
        // {
        //     get => PlayerPrefs.GetInt(SavedMoneyKey, 0);
        //     set => PlayerPrefs.SetInt(SavedMoneyKey, value);
        // }

    }
}