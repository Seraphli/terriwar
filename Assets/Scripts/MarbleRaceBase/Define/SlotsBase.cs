using System.Collections.Generic;
using MarbleRaceBase.Utility;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class SlotsBase : MonoBehaviour
    {
        public PaletteData paletteData;
        public Slot[] slots;
        public Slot defaultSlot;

        public string[] colorList;

        public List<Color> displayColors;

        protected virtual void SetupColorList()
        {
        }

        public void Setup()
        {
            SetupColorList();
            slots = new Slot[colorList.Length];
            for (int i = 0; i < colorList.Length; i++)
            {
                if (i == 0)
                {
                    var c = paletteData.GetDefault(colorList[i]);
                    slots[i] = new Slot(i, new Color[] {c, c, c});
                    displayColors.Add(c);
                }
                else
                {
                    var c = paletteData.Get(colorList[i]);
                    slots[i] = new Slot(i, c);
                    displayColors.AddRange(c);
                }
            }

            defaultSlot = slots[0];
        }
    }
}