using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace MarbleRaceBase.Define
{
    public class Tile : MonoBehaviour
    {
        public Slot slot;
        public float[] pos;
        [ReadOnly] public bool isCore = false;

        private SpriteRenderer _sr;

        public void SetPos(float i, float j)
        {
            pos = new[] {i, j};
        }

        public void SetSlot(Slot slot)
        {
            this.slot = slot;
            gameObject.layer = slot.layer;
            UpdateColor();
        }

        public void SetCore(bool b)
        {
            isCore = b;
            UpdateColor();
        }

        void UpdateColor()
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

        public void SetColor(Color c)
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