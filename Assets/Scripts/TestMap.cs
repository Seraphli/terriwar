using System;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    public TestGM gm;
    public float tileSize = 0.2f;
    public float backgroundSize = 10;
    public int baseSize = 4;
    public int coreSize = 2;
    public GameObject tile;
    public GameObject marble;

    void PlaceColor(GameObject go, int i, int j, int size)
    {
        int[] cases = {1, 2, 3, 4, 5, 6, 7, 8};
        var _case = Utils.CircleCase(i, j, size * 0.75f, baseSize);
        int team = _case + 5;
        if (Array.IndexOf(cases, _case) > -1)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = gm.data.teamMap[team].tileColor;
            go.layer = team;
            if (!gm.teamCount.ContainsKey(team))
            {
                gm.teamCount.Add(team, 0);
            }

            gm.teamCount[team] += 1;
        }

        _case = Utils.CircleCase(i, j, size * 0.75f, coreSize);
        if (Array.IndexOf(cases, _case) > -1)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = gm.data.teamMap[team].coreColor;
            var t = go.GetComponent<TestTile>();
            t.isCore = true;
            if (!gm.cores.ContainsKey(team))
            {
                gm.cores.Add(team, new List<GameObject>());
            }

            gm.cores[team].Add(go);
        }
    }

    void PlaceTiles()
    {
        var tileZ = tile.transform.position.z;
        tile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        var tiles = new GameObject("Tiles");
        var size = (int) Math.Floor(backgroundSize / 2 / tileSize) + 1;
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
        int[] place = {0, 3};
        foreach (var entry in gm.cores)
        {
            foreach (var i in place)
            {
                int team = entry.Key;
                var tilePos = entry.Value[i].transform.position;
                var pos = new Vector3(tilePos.x, tilePos.y, marbleZ);
                go = Instantiate(marble, pos, Quaternion.identity, marbles.transform);
                go.layer = team;
                var b = go.GetComponent<TestMarble>();
                b.gm = gm;
                b.team = team;
                b.Setup();
                var c = gm.data.teamMap[team].ballColor;
                var sr = go.GetComponent<SpriteRenderer>();
                sr.color = c;
                var tr = go.GetComponent<TrailRenderer>();
                tr.startColor = c;
                tr.endColor = new Color(c.r, c.g, c.b, 0f);
                if (!gm.marbles.ContainsKey(team))
                {
                    gm.marbles.Add(team, new List<GameObject>());
                }

                gm.marbles[team].Add(go);
            }
        }
    }

    public void Setup()
    {
        PlaceTiles();
        PlaceMarble();
    }
}