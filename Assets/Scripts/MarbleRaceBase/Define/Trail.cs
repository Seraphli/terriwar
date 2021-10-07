using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace MarbleRaceBase.Define
{
    public class Trail : MonoBehaviour
    {
        public float time = 0.2f;
        public Material mat;
        private TrailRenderer _tr;
        private SpriteRenderer _sr;
        
        private void Awake()
        {
            _tr = gameObject.AddComponent<TrailRenderer>();
            _tr.startWidth = 0.1f;
            _tr.endWidth = 0.1f;
            _tr.time = time;
            _tr.materials = new Material[] {mat};
            _tr.shadowCastingMode = ShadowCastingMode.Off;
            _tr.receiveShadows = false;
        }

        private void Update()
        {
            _sr = GetComponent<SpriteRenderer>();
            var c = _sr.color;
            _tr.startColor = c;
            _tr.endColor = new Color(c.r, c.g, c.b, 0);
        }
    }
}