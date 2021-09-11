using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TestBounce : MonoBehaviour
{
    public float radius = 0.1f;
    private List<Vector2> points = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        var s = sr.sprite;
        var c = s.GetPhysicsShapeCount();
        for (int i = 0; i < c; i++)
        {
            s.GetPhysicsShape(i, points);
            points.Add(points[0]);
            EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
            edge.points = points.ToArray();
            edge.edgeRadius = radius;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}