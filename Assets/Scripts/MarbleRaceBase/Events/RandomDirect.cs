using System;
using MarbleRaceBase.Define;
using MarbleRaceBase.Utility;
using UnityEngine;

namespace MarbleRaceBase.Events
{
    public class RandomDirect : TimerEvent
    {
        private GM _gm;

        private void Awake()
        {
            _gm = MonoUtils.GetGM();
        }

        public override void Trigger()
        {
            foreach (var entry in _gm.realTimeData.teams)
            {
                foreach (var m in entry.Value.marbles)
                {
                    m.SetSpeed(m.marbleData.initSpeed);
                }
            }
        }
    }
}