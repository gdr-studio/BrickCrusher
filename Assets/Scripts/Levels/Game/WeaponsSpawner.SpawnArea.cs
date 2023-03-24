using Merging;
using UnityEngine;
using Weapons;

namespace Levels.Game
{
    public partial class WeaponsSpawner
    {
        [System.Serializable]
        private class SpawnArea
        {
            public Vector3 Position;
            public Vector3 ScreenPosition;
            public CannonPlacement Placement;
            public CannonSpawnable SpawnedCannon;

            public bool IsFree = true;
            public void Free()
            {
                SpawnedCannon.Delete();
                FreeNoDel();
            }

            public void FreeNoDel()
            {
                Placement.Show();
                IsFree = true;
                SpawnedCannon = null;
            }
            
            public void Spawn(CannonSpawnable cannon)
            {
                SpawnedCannon = cannon;
                Placement.Hide();
                IsFree = false;
            }
        }
    }
}