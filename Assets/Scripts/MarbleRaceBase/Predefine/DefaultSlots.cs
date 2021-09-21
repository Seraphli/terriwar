using System.Collections.Generic;
using MarbleRaceBase.Define;
using MarbleRaceBase.Utility;
using Unity.Collections;
using UnityEngine;

namespace MarbleRaceBase.Predefine
{
    public class DefaultSlots : SlotsBase
    {
        protected override void SetupColorList()
        {
            colorList = new string[]
            {
                "grey", "red", "orange", "yellow", "green",
                "cyan", "blue", "purple", "pink"
            };
        }
    }
}