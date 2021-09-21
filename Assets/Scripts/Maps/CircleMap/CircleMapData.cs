using MarbleRaceBase.Define;
using UnityEngine;

namespace Maps.CircleMap
{
    public class CircleMapData : MapData
    {
        #region MapLayout

        [Header("MapLayout")] public float tileSize = 0.2f;
        public float backgroundSize = 8;
        public int baseSize = 4;
        public int baseShift = -5;
        public int coreSize = 2;
        public int coreShift = -5;
        public int[] marblePlaces = new[] {0, 3};

        #endregion


        #region Prefab

        [Header("Prefab")] public GameObject tile;
        public GameObject marble;

        #endregion


        public void Setup()
        {
            teams = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
        }
    }
}