using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using MarbleRaceBase.Define;
using UnityEngine;


public class ShootGM : GM
{
    public GameObject canvas;
    public TestData data;
    public TestBounce bounce;
    public TestRank rank;
    public TestRule rule;
    [ReadOnly] public int tileNum;
    [ReadOnly] public List<int> teamCountList;

    public Dictionary<int, int> teamCount = new Dictionary<int, int>();
    public Dictionary<int, List<GameObject>> cores = new Dictionary<int, List<GameObject>>();
    public Dictionary<int, GameObject> cannons = new Dictionary<int, GameObject>();
    public Dictionary<int, List<GameObject>> marbles = new Dictionary<int, List<GameObject>>();

    // Start is called before the first frame update
    void Awake()
    {
        if (!Application.isEditor)
        {
            Cursor.visible = false;
        }

        data.Setup();
        bounce.Setup();
        map.Setup();
    }


    void EliminateMarble(int team)
    {
        foreach (var go in marbles[team])
        {
            go.GetComponent<TestMarble>().SelfDestruction();
        }

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
        // 处理有灰色方块的情况, 黑色方块不在
        if (teamCount.ContainsKey(oldTeam))
        {
            teamCount[oldTeam] -= 1;
        }

        teamCount[newTeam] += 1;
    }


    // Update is called once per frame
    void Update()
    {
        teamCountList = new List<int>();
        foreach (var item in teamCount)
        {
            teamCountList.Add(item.Value);
        }
    }
}