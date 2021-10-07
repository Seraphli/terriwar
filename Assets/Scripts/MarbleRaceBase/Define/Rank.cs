using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MarbleRaceBase.Define;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public GM gm;
    public int rankNum;
    public float lineSpace;
    public GameObject canvas;
    public GameObject rankLine;


    public List<RankLine> ranks = new List<RankLine>();

    public void Setup(int num)
    {
        rankNum = num;
        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(rankLine, canvas.transform);
            var rt = go.GetComponent<RectTransform>();
            rt.localPosition += Vector3.down * (rt.rect.height + lineSpace) * i;
            var trl = go.GetComponent<RankLine>();
            trl.SetRank(i + 1);
            ranks.Add(trl);
        }
    }

    void Update()
    {
        var sorted = gm.realTimeData.teams.OrderByDescending(x => x.Value.tileCount).ToList();
        int idx = 0;
        foreach (var item in sorted)
        {
            var r = ranks[idx];
            r.SetColor(item.Value.slot.slotColor.tileColor);
            r.SetNum(item.Value.tileCount);
            idx += 1;
        }
    }
}