using MarbleRaceBase.Define;
using UnityEngine;

namespace Maps.ShootMap
{
    public class ShootMapData : MapData
    {
        #region MapLayout

        [Header("MapLayout")] public float tileSize = 0.2f;
        public float backgroundSize = 8;
        public int baseSize = 6;
        public int baseShift = -3;
        public int coreSize = 2;
        public int coreShift = -1;

        #endregion


        #region Prefab

        [Header("Prefab")] public GameObject tile;
        public GameObject cannon;

        #endregion


        public void Setup()
        {
            teams = new int[] {0, 2, 4, 6, 8};
        }
    }
}