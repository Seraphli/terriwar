using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestMarble : MonoBehaviour
{
    public int team;
    public TestGM gm;

    public float setupSec = 5;
    public int destroyTick = 50;
    public float destroySec = 1;
    public float destroyScale = 10;

    private Rigidbody2D _rb;
    private float _speed = 0;
    private float _initSpeed;

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

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(setupSec);
        SetSpeed(_initSpeed);
    }

    public void Setup()
    {
        StartCoroutine(WaitFor());
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initSpeed = gm.rule.initSpeed;
    }

    IEnumerator Explode()
    {
        Destroy(GetComponent<CircleCollider2D>());
        SetSpeed(0);
        var sr = GetComponent<SpriteRenderer>();
        var c = sr.color;
        for (int i = 0; i < destroyTick; i++)
        {
            transform.localScale += Vector3.one * destroyScale / destroyTick;
            sr.color = new Color(c.r, c.g, c.b, (destroyTick - i) * 1f / destroyTick);
            yield return new WaitForSeconds(destroySec / destroyTick);
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
            var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
            _rb.velocity = mNewForce.normalized * _speed;
        }

        _rb.velocity = _rb.velocity.normalized * _speed;
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