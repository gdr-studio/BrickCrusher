using UnityEngine;

namespace Statues
{
    [CreateAssetMenu(fileName = nameof(StatueElementSettings), menuName ="SO/" + nameof(StatueElementSettings))]
    public class StatueElementSettings : ScriptableObject
    {
        public int Health;
    }
}