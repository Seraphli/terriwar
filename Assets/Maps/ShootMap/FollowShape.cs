using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShape : MonoBehaviour
{
    public Transform target;
    private RectTransform self;

    private void Start()
    {
        self = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        var pos = target.localPosition;
        self.localPosition = new Vector3(pos.x, pos.y, self.localPosition.z);
        var scale = target.localScale;
        self.sizeDelta = new Vector2(scale.x, scale.y);
    }
}
