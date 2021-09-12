using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TestBounce : MonoBehaviour
{
    public float extra = 0.05f;
    public float radius = 0.1f;
    private List<Vector2> points = new List<Vector2>();

    // Start is called before the first frame update
    public void Setup()
    {
        var bound = Instantiate(gameObject, transform.position, Quaternion.identity);
        Destroy(bound.GetComponent<SpriteMask>());
        bound.transform.localScale = transform.localScale + Vector3.one * (radius * 2 + extra);
        var sr = bound.GetComponent<SpriteRenderer>();
        var s = sr.sprite;
        var c = s.GetPhysicsShapeCount();
        for (int i = 0; i < c; i++)
        {
            s.GetPhysicsShape(i, points);
            points.Add(points[0]);
            EdgeCollider2D edge = bound.AddComponent<EdgeCollider2D>();
            edge.points = points.ToArray();
            edge.edgeRadius = radius;
        }

        Destroy(sr);
    }

    // Update is called once per frame
    void Update()
    {
    }
}