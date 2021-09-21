using System;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class Timer : MonoBehaviour
    {
        public float initSecond;
        public float curTime;
        public TimerEvent[] events;

        public void Setup()
        {
            curTime = initSecond;
        }

        private void Update()
        {
            curTime += Time.deltaTime;
            foreach (var e in events)
            {
                if (e.enable && e.second < curTime)
                {
                    e.Trigger();
                }
                
            }
        }
    }
}