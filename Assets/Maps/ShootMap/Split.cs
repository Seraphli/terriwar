using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public Transform time2;

    public Transform split;

    public Transform release;

    public float total;
    public Vector2 leftAnchor;
    public float minP = 0.3f;
    public float maxP = 0.7f;
    public float percentage = 0.3f;
    private float curPercentage;

    public void SetPercentage(float p)
    {
        if (p > maxP)
        {
            p = maxP;
        }

        if (p < minP)
        {
            p = minP;
        }

        curPercentage = p;
        var t2 = p * total;
        time2.localScale = new Vector3(t2, time2.localScale.y, time2.localScale.z);
        time2.localPosition = new Vector3(leftAnchor.x + time2.localScale.x / 2,
            time2.localPosition.y, time2.localPosition.z);
        split.localPosition =
            new Vector3(leftAnchor.x + time2.localScale.x + split.localScale.y / 2,
                split.localPosition.y, split.localPosition.z);
        release.localScale =
            new Vector3(total - t2, release.localScale.y, release.localScale.z);
        release.localPosition =
            new Vector3(
                leftAnchor.x + time2.localScale.x + split.localScale.y +
                release.localScale.x / 2, release.localPosition.y,
                release.localPosition.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        leftAnchor = new Vector2(time2.localPosition.x - time2.localScale.x / 2,
            time2.localPosition.y);
        total = time2.localScale.x + release.localScale.x;
    }
}