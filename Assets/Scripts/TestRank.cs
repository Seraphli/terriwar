using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRank : MonoBehaviour
{
    public int rankNum;
    public float lineSpace;
    public GameObject canvas;
    public GameObject rankLine;


    public List<TestRankLine> ranks = new List<TestRankLine>();

    public void Setup(int num)
    {
        rankNum = num;
        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(rankLine, canvas.transform);
            var rt = go.GetComponent<RectTransform>();
            rt.localPosition += Vector3.down * (rt.rect.height + lineSpace) * i;
            var trl = go.GetComponent<TestRankLine>();
            trl.SetRank(i + 1);
            ranks.Add(trl);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}