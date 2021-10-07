using MarbleRaceBase.Utility;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class CannonBall : MonoBehaviour
    {
        private GM _gm;
        public Slot slot;
        public float curSpeed;

        private SpriteRenderer _sr;
        private Rigidbody2D _rb;
        private TrailRenderer _tr;

        public void SetSlot(Slot slot)
        {
            this.slot = slot;
            gameObject.layer = slot.layer;
            var c = slot.slotColor.ballColor;
            _sr = GetComponent<SpriteRenderer>();
            _sr.color = c;
            _tr = GetComponent<TrailRenderer>();
            _tr.startColor = c;
            _tr.endColor = new Color(c.r, c.g, c.b, 0f);
        }

        void Start()
        {
            _gm = MonoUtils.GetGM();
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _tr = GetComponent<TrailRenderer>();
        }

        void FixedUpdate()
        {
            if (curSpeed > 0 && _rb.velocity.magnitude < 0.001f)
            {
                var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f),
                    Random.Range(-10.0f, 10.0f));
                _rb.velocity = mNewForce.normalized * curSpeed;
            }

            _rb.velocity = _rb.velocity.normalized * curSpeed;
        }

        void OnCollisionExit2D(Collision2D col)
        {
            var go = col.gameObject;
            if (go.GetComponent<Cannon>())
            {
                Destroy(go);
                Destroy(gameObject);
            }

            var t = go.GetComponent<Tile>();
            if (t)
            {
                _gm.realTimeData.UpdateCount(t.slot.index, slot.index);
                t.SetSlot(slot);
                t.UpdateColor();
                Destroy(gameObject);
            }
        }
    }
}