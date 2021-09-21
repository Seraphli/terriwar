using System.Collections;
using MarbleRaceBase.Utility;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class Marble : MonoBehaviour
    {
        public Slot slot;
        private float _speed = 0;

        private GM _gm;
        private MarbleData _marbleData;
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

        public float SetSpeed(float s)
        {
            _speed = s;
            return _speed;
        }

        public float ScaleSpeed(float scale)
        {
            SetSpeed(_speed * scale);
            return _speed;
        }

        void Awake()
        {
            _gm = MonoUtils.GetGM();
            _marbleData = _gm.mapData.marbleData;
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _tr = GetComponent<TrailRenderer>();
        }

        IEnumerator Explode()
        {
            Destroy(GetComponent<CircleCollider2D>());
            SetSpeed(0);
            var c = _sr.color;
            for (int i = 0; i < _marbleData.destroyTick; i++)
            {
                transform.localScale += Vector3.one * _marbleData.destroyScale /
                                        _marbleData.destroyTick;
                _sr.color = new Color(c.r, c.g, c.b,
                    (_marbleData.destroyTick - i) * 1f / _marbleData.destroyTick);
                yield return new WaitForSeconds(_marbleData.destroySec /
                                                _marbleData.destroyTick);
            }

            Destroy(gameObject);
        }

        public void SelfDestruction()
        {
            StartCoroutine(Explode());
        }

        void FixedUpdate()
        {
            if (_speed > 0 && _rb.velocity.magnitude < 0.001f)
            {
                var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f),
                    Random.Range(-10.0f, 10.0f));
                _rb.velocity = mNewForce.normalized * _speed;
            }

            _rb.velocity = _rb.velocity.normalized * _speed;
        }

        void OnCollisionExit2D(Collision2D col)
        {
            var go = col.gameObject;
            var t = go.GetComponent<Tile>();
            if (t && t.slot.index > 0)
            {
                if (t.isCore)
                {
                    t.SetCore(false);
                    _gm.realTimeData.teams[t.slot.index].RemoveCore(t);
                }

                _gm.realTimeData.UpdateCount(t.slot.index, slot.index);
                t.SetSlot(slot);
            }
            // TODO: 增加一个callback函数队列
        }
    }
}