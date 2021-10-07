using System;
using System.Collections;
using UnityEngine;

namespace Maps.ShootMap
{
    public class ShootRule : MonoBehaviour
    {
        public MarbleRaceBase.Define.Cannon[] cannons;
        public float startTime = 5f;
        public float increaseAmmoTime = 60f;

        IEnumerator Shoot()
        {
            yield return new WaitForSeconds(startTime);
            foreach (var c in cannons)
            {
                c.isShoot = true;
            }

            yield return null;
        }

        IEnumerator Increase()
        {
            yield return new WaitForSeconds(startTime);
            while (true)
            {
                yield return new WaitForSeconds(increaseAmmoTime);
                foreach (var c in cannons)
                {
                    c.maxAmmo = Math.Min(4096, c.maxAmmo * 2);
                    c.shootCD = Math.Max(0.01f, c.shootCD - 0.01f);
                }
            }
        }

        private void Start()
        {
            StartCoroutine(Shoot());
            StartCoroutine(Increase());
        }
    }
}