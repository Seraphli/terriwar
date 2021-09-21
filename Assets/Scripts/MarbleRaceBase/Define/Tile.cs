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

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

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
                _sr.color = slot.slotColor.coreColor;
            }
            else
            {
                _sr.color = slot.slotColor.tileColor;
            }
        }
    }
}