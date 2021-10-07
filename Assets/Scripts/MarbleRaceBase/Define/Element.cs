using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class Element : MonoBehaviour
    {
        public Slot slot;
        public Vector2 pos;
        public Vector3 realPos;

        public virtual void SetSlot(Slot slot)
        {
            this.slot = slot;
            gameObject.layer = slot.layer;
        }

        public virtual void SetPos(float i, float j)
        {
            pos = new Vector2(i, j);
        }

        public virtual void SetRealPos(Vector3 realPos)
        {
            this.realPos = realPos;
        }

        public virtual void SetColor(Color c)
        {
        }

        public virtual void SetMaskInter(SpriteMaskInteraction v)
        {
        }

        public virtual void SetParams(int common, int special)
        {
        }
    }
}