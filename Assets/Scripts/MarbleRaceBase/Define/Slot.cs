using System;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    [Serializable]
    public class SlotColor
    {
        public Color ballColor;
        public Color tileColor;
        public Color coreColor;

        public SlotColor(Color[] colors)
        {
            ballColor = colors[0];
            tileColor = colors[1];
            coreColor = colors[2];
        }
    }
    
    [Serializable]
    public class Slot
    {
        public int index;
        public int layer;
        public SlotColor slotColor;

        public Slot(int index, Color[] c)
        {
            this.index = index;
            layer = index + 6;
            slotColor = new SlotColor(c);
            Physics2D.IgnoreLayerCollision(layer, layer);
        }
    }
}