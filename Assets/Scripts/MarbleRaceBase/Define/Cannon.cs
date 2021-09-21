using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class Cannon : MonoBehaviour
    {
        public Slot slot;
        public GameObject marble;
        public SpriteRenderer[] srs;

        public void SetSlot(Slot slot)
        {
            this.slot = slot;
            UpdateColor(slot.slotColor.ballColor);
        }

        public void UpdateColor(Color c)
        {
            foreach (var sr in srs)
            {
                sr.color = c;
            }
        }
    }
}