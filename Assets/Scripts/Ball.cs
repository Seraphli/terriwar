using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public int type;
    public float speed;
    public bool debug = false;

    public Color color;
    public GM gm;
    private bool _enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        color = gm.colorMap[type];
        var sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(color.r * 0.6f, color.g * 0.6f, color.b * 0.6f);
        var rb = GetComponent<Rigidbody2D>();
        var m_NewForce = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        rb.velocity = m_NewForce.normalized * speed;
        _enabled = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_enabled)
        {
            return;
        }

        var rb = GetComponent<Rigidbody2D>();
        if (debug)
        {
            print(rb.velocity);
        }

        rb.velocity = rb.velocity.normalized * speed;

        if (debug)
        {
            print(rb.velocity);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (!_enabled)
        {
            return;
        }

        var go = col.gameObject;
        if (go.name.Contains("Square") && go.layer >= 6)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = color;
            go.layer = type;
        }
    }
}