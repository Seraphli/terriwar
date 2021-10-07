using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public ShootTeam st;
    public int type;

    private void Start()
    {
        st = transform.parent.parent.GetComponent<ShootTeam>();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (st.c.isShoot)
        {
            return;
        }
        var go = col.gameObject;
        go.transform.localPosition = new Vector3(0, 1.3f);
        if (type == 0)
        {
            st.c.ammo = Math.Min(st.c.maxAmmo, st.c.ammo * 2);
        }
        else
        {
            st.Shoot();
        }
    }
}
