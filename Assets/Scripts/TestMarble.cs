using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestMarble : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public int team;
    public TestGM gm;
    private Rigidbody2D _rb;

    public void SetSpeed(float s)
    {
        speed = Math.Min(s, maxSpeed);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        _rb.velocity = mNewForce.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (_rb.velocity.magnitude < 0.001f)
        {
            var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
            _rb.velocity = mNewForce.normalized * speed;
        }

        _rb.velocity = _rb.velocity.normalized * speed;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        var go = col.gameObject;
        if (go.name.Contains("Tile") && go.layer >= 6)
        {
            gm.ChangeColor(go.layer, gameObject.layer);
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = gm.data.teamMap[team].tileColor;
            go.layer = gameObject.layer;
            var t = go.GetComponent<TestTile>();
            if (t.isCore)
            {
                t.isCore = false;
                gm.EliminateCore(go);
            }

            gm.rule.IncrGlobalProcess();
        }
    }
}