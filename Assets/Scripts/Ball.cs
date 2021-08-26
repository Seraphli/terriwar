using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int type;

    public Color color;
    public GM gm;

    // Start is called before the first frame update
    void Start()
    {
        color = gm.colorMap[type];
        var sr = GetComponent<SpriteRenderer>();
        sr.color = color;
        var rb = GetComponent<Rigidbody2D>();
        var m_NewForce = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        rb.AddForce(m_NewForce.normalized, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var go = col.gameObject;
        if (go.name.Contains("Square") && go.layer >= 6)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = color;
            go.layer = type;
        }
    }
}