using Merging;
using Weapons;

namespace Levels.Game
{
    public partial class WeaponsSpawner
    {
        [System.Serializable]
        private class TackingData
        {
            public SpawnArea currentArea;
            public CannonSpawnable Spawnable;
            public MergingData mergingData;
            public MergingItemArea fromArea;

            public void SetArea(SpawnArea area)
            {
                currentArea = area;
                Spawnable.transform.position = area.Position;
                area.Spawn(Spawnable);
            }
        }
    }
}