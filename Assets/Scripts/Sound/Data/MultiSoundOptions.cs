using Data;

namespace Game.Sound.Data
{
    [System.Serializable]
    public class MultiSoundOptions : TWeightedPicker<SoundData>
    {
        public ESoundType Type;
        
    }
}