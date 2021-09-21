using System;
using MarbleRaceBase.Define;
using MarbleRaceBase.Utility;
using Unity.Collections;
using UnityEngine;

namespace Maps.CircleMap
{
    public class CircleMapRule : Rule
    {
        [Header("Global")]
        public int totalGlobalProgress;
        public int totalGlobalProgressStep;
        [ReadOnly] public int curGlobalProgress;
        public float speedScale = 1.5f;

        private GM _gm;

        public void IncrSpeed()
        {
            foreach (var item in _gm.realTimeData.teams)
            {
                foreach (var marble in item.Value.marbles)
                {
                    marble.ScaleSpeed(speedScale);
                    print($"{marble.GetInstanceID()} {marble.marbleData.curSpeed}");
                }
            }
        }

        public void IncrGlobalProcess()
        {
            curGlobalProgress += 1;
            if (curGlobalProgress == totalGlobalProgress)
            {
                curGlobalProgress = 0;
                IncrSpeed();
                totalGlobalProgress += totalGlobalProgressStep;
            }
        }

        private void Awake()
        {
            _gm = MonoUtils.GetGM();
        }

        public override void Collide(GameObject marble, GameObject tile)
        {
            IncrGlobalProcess();
        }
    }
}