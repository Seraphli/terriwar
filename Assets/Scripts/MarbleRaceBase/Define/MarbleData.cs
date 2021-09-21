using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class MarbleData : MonoBehaviour
    {
        public float initSpeed = 2;
        public float maxSpeed = 20;
        public float curSpeed;
        public int destroyTick = 50;
        public float destroySec = 1;
        public float destroyScale = 10;
    }
}