using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace MarbleRaceBase.Define
{
    public class Tile : Element
    {
        [ReadOnly] public bool isCore = false;

        private SpriteRenderer _sr;

        public void SetCore(bool b)
        {
            isCore = b;
            // UpdateColor();
        }

        public void UpdateColor()
        {
            if (isCore)
            {
                SetColor(slot.slotColor.coreColor);
            }
            else
            {
                SetColor(slot.slotColor.tileColor);
            }
        }

        void GetSR()
        {
            if (_sr == null)
            {
                _sr = GetComponent<SpriteRenderer>();
            }
        }

        public override void SetColor(Color c)
        {
            GetSR();
            _sr.color = c;
        }

        public void SetMaskInter(SpriteMaskInteraction v)
        {
            GetSR();
            _sr.maskInteraction = v;
        }
    }
}