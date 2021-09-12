using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;


public class TestGM : MonoBehaviour
{
    public GameObject canvas;
    public TestData data;
    public GameObject tile;
    public GameObject marble;
    public TestBounce bounce;
    public TestRank rank;
    public TestRule rule;
    public float tileSize;
    public float backgroundSize;
    public int baseSize = 4;
    public int coreSize = 2;
    public int destroyTick = 50;
    public float destroySeconds = 10;
    public float destroyScale = 10;
    [ReadOnly] public int tileNum;
    [ReadOnly] public List<int> teamCountList;

    public Dictionary<int, int> teamCount = new Dictionary<int, int>();
    public Dictionary<int, List<GameObject>> cores = new Dictionary<int, List<GameObject>>();
    public Dictionary<int, List<GameObject>> marbles = new Dictionary<int, List<GameObject>>();

    // In range
    bool In(int a, int v, int b)
    {
        return v >= a && v < b;
    }

    int CircleCase(int i, int j, float radius, int range)
    {
        float factor = Mathf.Sqrt(2f) / 2f;
        int halfRange = range / 2;
        // 顺时针
        // 上
        if (In(-halfRange, i, halfRange) &&
            In(-halfRange, j - (int) radius, halfRange))
        {
            return 1;
        }

        if (In(-halfRange, i - (int) (radius * factor), halfRange) &&
            In(-halfRange, j - (int) (radius * factor), halfRange))
        {
            return 2;
        }

        // 右
        if (In(-halfRange, i - (int) radius, halfRange) &&
            In(-halfRange, j, halfRange))
        {
            return 3;
        }

        if (In(-halfRange, i - (int) (radius * factor), halfRange) &&
            In(-halfRange, j + (int) (radius * factor), halfRange))
        {
            return 4;
        }

        // 下
        if (In(-halfRange, i, halfRange) &&
            In(-halfRange, j + (int) radius, halfRange))
        {
            return 5;
        }

        if (In(-halfRange, i + (int) (radius * factor), halfRange) &&
            In(-halfRange, j + (int) (radius * factor), halfRange))
        {
            return 6;
        }

        // 左
        if (In(-halfRange, i + (int) radius, halfRange) &&
            In(-halfRange, j, halfRange))
        {
            return 7;
        }

        if (In(-halfRange, i + (int) (radius * factor), halfRange) &&
            In(-halfRange, j - (int) (radius * factor), halfRange))
        {
            return 8;
        }

        return 0;
    }

    void PlaceColor(GameObject go, int i, int j, int size)
    {
        int[] cases = {1, 2, 3, 4, 5, 6, 7, 8};
        var _case = CircleCase(i, j, size * 0.75f, baseSize);
        int team = _case + 5;
        if (Array.IndexOf(cases, _case) > -1)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = data.teamMap[team].tileColor;
            go.layer = team;
            if (!teamCount.ContainsKey(team))
            {
                teamCount.Add(team, 0);
            }

            teamCount[team] += 1;
        }

        _case = CircleCase(i, j, size * 0.75f, coreSize);
        if (Array.IndexOf(cases, _case) > -1)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = data.teamMap[team].coreColor;
            var t = go.GetComponent<TestTile>();
            t.isCore = true;
            if (!cores.ContainsKey(team))
            {
                cores.Add(team, new List<GameObject>());
            }

            cores[team].Add(go);
        }
    }

    void PlaceTiles()
    {
        var tileZ = tile.transform.position.z;
        tile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        var tiles = new GameObject("Tiles");
        var size = (int) Math.Floor(backgroundSize / 2 / tileSize) + 1;
        tileNum = size * 2;
        GameObject go;
        for (int i = -size; i < size; i++)
        {
            for (int j = -size; j < size; j++)
            {
                var pos = new Vector3((i + 0.5f) * tileSize, (j + 0.5f) * tileSize, tileZ);
                go = Instantiate(tile, pos, Quaternion.identity, tiles.transform);
                go.layer = 14;
                PlaceColor(go, i, j, size);
            }
        }
    }

    void PlaceMarble()
    {
        var marbleZ = marble.transform.position.z;
        var marbles = new GameObject("Marbles");
        GameObject go;
        foreach (var entry in cores)
        {
            for (int i = 0; i < entry.Value.Count; i += 2)
            {
                int team = entry.Key;
                var tilePos = entry.Value[i].transform.position;
                var pos = new Vector3(tilePos.x, tilePos.y, marbleZ);
                go = Instantiate(marble, pos, Quaternion.identity, marbles.transform);
                go.layer = team;
                var b = go.GetComponent<TestMarble>();
                b.gm = this;
                b.team = team;
                var c = data.teamMap[team].ballColor;
                var sr = go.GetComponent<SpriteRenderer>();
                sr.color = c;
                var tr = go.GetComponent<TrailRenderer>();
                tr.startColor = c;
                tr.endColor = new Color(c.r, c.g, c.b, 0f);
                if (!this.marbles.ContainsKey(team))
                {
                    this.marbles.Add(team, new List<GameObject>());
                }

                this.marbles[team].Add(go);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        data.Setup();
        bounce.Setup();
        PlaceTiles();
        PlaceMarble();
        rank.Setup(cores.Count);
        rule.Setup(cores.Count);
    }

    IEnumerator Explode(GameObject m)
    {
        Destroy(m.GetComponent<CircleCollider2D>());
        var tm = m.GetComponent<TestMarble>();
        tm.SetSpeed(0);
        var sr = m.GetComponent<SpriteRenderer>();
        var c = sr.color;
        for (int i = 0; i < destroyTick; i++)
        {
            m.transform.localScale += Vector3.one * destroyScale / destroyTick;
            sr.color = new Color(c.r, c.g, c.b, (destroyTick - i) * 1f / destroyTick);
            yield return new WaitForSeconds(destroySeconds / destroyTick);
        }

        Destroy(m);

        yield return null;
    }


    void EliminateMarble(int team)
    {
        foreach (var go in marbles[team])
        {
            StartCoroutine(Explode(go));
        }

        rule.teamPB[team].SetText("0");
        rule.teamPB[team].SetProgress(0f);
        rule.teamPB[team].SetColor(Color.black);

        marbles.Remove(team);
    }

    public void EliminateCore(GameObject go)
    {
        int team = -1;
        foreach (var item in cores)
        {
            item.Value.Remove(go);
            if (item.Value.Count == 0)
            {
                team = item.Key;
                EliminateMarble(item.Key);
            }
        }

        if (team != -1)
        {
            cores.Remove(team);
        }
    }

    public void ChangeColor(int oldTeam, int newTeam)
    {
        if (teamCount.ContainsKey(oldTeam))
        {
            teamCount[oldTeam] -= 1;
        }

        teamCount[newTeam] += 1;
    }

    public float IncrSpeed()
    {
        float newSpeed = -1;
        foreach (var item in marbles)
        {
            foreach (var marble in item.Value)
            {
                var m = marble.GetComponent<TestMarble>();
                if (newSpeed < 0)
                {
                    newSpeed = m.speed * rule.speedScale;
                }

                m.SetSpeed(newSpeed);
            }
        }

        return newSpeed;
    }

    public int MarbleFission(int team)
    {
        if (!marbles.ContainsKey(team))
        {
            return 0;
        }

        var mars = marbles[team];
        var count = mars.Count;
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(mars[i]);
            mars.Add(go);
        }

        return mars.Count;
    }

    // Update is called once per frame
    void Update()
    {
        teamCountList = new List<int>();
        foreach (var item in teamCount)
        {
            teamCountList.Add(item.Value);
        }

        var sorted = teamCount.OrderByDescending(x => x.Value).ToList();
        int idx = 0;
        foreach (var item in sorted)
        {
            var r = rank.ranks[idx];
            r.SetColor(data.teamMap[item.Key].tileColor);
            r.SetNum(item.Value);
            idx += 1;
        }
    }
}