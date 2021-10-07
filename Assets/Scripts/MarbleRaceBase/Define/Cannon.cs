using System;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    public class Cannon : Element
    {
        public GameObject cannonBall;
        private float _startZ;
        private float _endZ;
        private float _t;
        public float rotateZ = 90;
        public float rotateSpeed = 0.1f;
        public float shootCD = 0.1f;
        private float curShootCD = 0f;
        public int maxAmmo = 2048;

        public int ammo = 1;
        public bool isShoot = false;

        public override void SetSlot(Slot slot)
        {
            this.slot = slot;
            gameObject.layer = slot.layer;
            foreach (Transform child in transform)
            {
                child.gameObject.layer = slot.layer;
            }
        }

        public override void SetColor(Color c)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<SpriteRenderer>().color = c;
            }
        }

        public override void SetParams(int common, int special)
        {
            float degree = (special & 0xF) * 90f;
            transform.Rotate(0, 0, degree);
        }

        private void Start()
        {
            _startZ = transform.localRotation.eulerAngles.z;
            _endZ = _startZ + rotateZ;
            _t = 0f;
        }

        void Shoot()
        {
            var b = Instantiate(cannonBall,
                new Vector3(realPos.x, realPos.y, cannonBall.transform.position.z),
                Quaternion.identity);
            var cb = b.GetComponent<CannonBall>();
            cb.SetSlot(slot);
            var rb = b.GetComponent<Rigidbody2D>();
            var dir = transform.localRotation * Vector3.right;
            rb.AddForce(dir, ForceMode2D.Impulse);
        }

        private void Update()
        {
            _t += rotateSpeed * Time.deltaTime;
            var angle = Mathf.Lerp(_startZ, _endZ, _t);
            transform.localRotation = Quaternion.identity;
            transform.Rotate(0, 0, angle);
            if (_t > 1f)
            {
                float tmp = _startZ;
                _startZ = _endZ;
                _endZ = tmp;
                _t = 0f;
            }

            if (isShoot && ammo == 0)
            {
                isShoot = false;
                ammo = 1;
            }

            if (!isShoot && ammo >= maxAmmo)
            {
                isShoot = true;
            }

            if (isShoot)
            {
                if (curShootCD == 0)
                {
                    Shoot();
                    ammo -= 1;
                }

                curShootCD += Time.deltaTime;
                if (curShootCD > shootCD)
                {
                    curShootCD = 0f;
                }
            }
        }
    }
}