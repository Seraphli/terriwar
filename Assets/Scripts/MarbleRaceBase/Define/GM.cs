using System;
using UnityEngine;
using UnityEngine.Events;

namespace MarbleRaceBase.Define
{
    public class GM : MonoBehaviour
    {
        [Header("Map Shape")] public Map map;
        [Header("Map Data")] public MapData mapData;
        public Rule rule;
        [Header("Team information")] public RealTimeData realTimeData;

        public UnityEvent[] methods;

        private void Start()
        {
            if (methods != null)
            {
                foreach (var m in methods)
                {
                    m.Invoke();
                }
            }
        }
    }
}