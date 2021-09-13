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
    public TestMap map;
    public TestBounce bounce;
    public TestRank rank;
    public TestRule rule;
    [ReadOnly] public int tileNum;
    [ReadOnly] public List<int> teamCountList;

    public Dictionary<int, int> teamCount = new Dictionary<int, int>();
    public Dictionary<int, List<GameObject>> cores = new Dictionary<int, List<GameObject>>();
    public Dictionary<int, List<GameObject>> marbles = new Dictionary<int, List<GameObject>>();

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        data.Setup();
        bounce.Setup();
        map.Setup();
        rank.Setup(cores.Count);
        rule.Setup(cores.Count);
    }


    void EliminateMarble(int team)
    {
        foreach (var go in marbles[team])
        {
            go.GetComponent<TestMarble>().SelfDestruction();
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