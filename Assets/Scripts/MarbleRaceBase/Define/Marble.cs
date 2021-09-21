using System.Collections;
using MarbleRaceBase.Utility;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class Marble : MonoBehaviour
    {
        public Slot slot;
        public MarbleData marbleData;

        private GM _gm;
        private SpriteRenderer _sr;
        private Rigidbody2D _rb;
        private TrailRenderer _tr;

        public void SetSlot(Slot slot)
        {
            this.slot = slot;
            gameObject.layer = slot.layer;
            var c = slot.slotColor.ballColor;
            _sr.color = c;
            _tr.startColor = c;
            _tr.endColor = new Color(c.r, c.g, c.b, 0f);
        }

        public void SetSpeed(float s)
        {
            marbleData.curSpeed = Mathf.Min(s, marbleData.maxSpeed);
        }

        public void ScaleSpeed(float scale)
        {
            SetSpeed(marbleData.curSpeed * scale);
        }

        void Awake()
        {
            _gm = MonoUtils.GetGM();
            marbleData = GetComponent<MarbleData>();
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _tr = GetComponent<TrailRenderer>();
        }

        IEnumerator Explode()
        {
            Destroy(GetComponent<CircleCollider2D>());
            SetSpeed(0);
            var c = _sr.color;
            for (int i = 0; i < marbleData.destroyTick; i++)
            {
                transform.localScale += Vector3.one * marbleData.destroyScale /
                                        marbleData.destroyTick;
                _sr.color = new Color(c.r, c.g, c.b,
                    (marbleData.destroyTick - i) * 1f / marbleData.destroyTick);
                yield return new WaitForSeconds(marbleData.destroySec /
                                                marbleData.destroyTick);
            }

            Destroy(gameObject);
        }

        public void SelfDestruction()
        {
            StartCoroutine(Explode());
        }

        void FixedUpdate()
        {
            if (marbleData.curSpeed > 0 && _rb.velocity.magnitude < 0.001f)
            {
                var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f),
                    Random.Range(-10.0f, 10.0f));
                _rb.velocity = mNewForce.normalized * marbleData.curSpeed;
            }

            print($"{GetInstanceID()} {marbleData.curSpeed}");
            _rb.velocity = _rb.velocity.normalized * marbleData.curSpeed;
        }

        void OnCollisionExit2D(Collision2D col)
        {
            var go = col.gameObject;
            var t = go.GetComponent<Tile>();
            if (t)
            {
                if (t.isCore)
                {
                    t.SetCore(false);
                    _gm.realTimeData.teams[t.slot.index].RemoveCore(t);
                }

                _gm.realTimeData.UpdateCount(t.slot.index, slot.index);
                t.SetSlot(slot);
            }

            _gm.rule.Collide(gameObject, go);
        }
    }
}