using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{
    public float speed;

    private bool _enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        var m_NewForce = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        rb.velocity = m_NewForce.normalized * speed;
        _enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!_enabled)
        {
            return;
        }

        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = rb.velocity.normalized * speed;
    }
}