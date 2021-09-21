using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class TimerEvent : MonoBehaviour
    {
        public float second;
        public bool enable = true;

        public virtual void Trigger()
        {
            enable = false;
        }
    }
}