using System;
using UnityEngine;

namespace Maps.ShootMap
{
    public class UpdateSplit : MonoBehaviour
    {
        public ShootTeam st;
        public Split split;
        public float maxTiles = 1600f;
        public float curProgress = 0;
        public float maxProgress = 1800f;

        private void Update()
        {
            curProgress += Time.deltaTime;
            var p = 1 - Math.Min(st.tileCount, maxTiles) / maxTiles +
                    Math.Min(curProgress, maxProgress) / maxProgress;
            split.SetPercentage(p);
        }
    }
}