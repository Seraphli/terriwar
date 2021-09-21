using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ShootRule : MonoBehaviour
{
    public TestGM gm;
    public int totalGlobalProgress;
    public int totalGlobalProgressStep;
    public float totalTeamProgress;
    public float totalTeamProgressStep;
    public float teamScale = 0.01f;
    public float initSpeed = 2;
    public float maxSpeed = 20;
    public float curSpeed;
    public float speedScale = 1.5f;

    [ReadOnly] public int curGlobalProgress;
    public Dictionary<int, float> curTeamProgress = new Dictionary<int, float>();
    public Dictionary<int, float> totalTeamsProgress = new Dictionary<int, float>();
    public TestProgressBar globalPB;
    public Dictionary<int, TestProgressBar> teamPB = new Dictionary<int, TestProgressBar>();
    public GameObject pbPrefab;

    public void IncrGlobalProcess()
    {
        curGlobalProgress += 1;
        if (curGlobalProgress == totalGlobalProgress)
        {
            curGlobalProgress = 0;
            IncrSpeed();
            globalPB.SetText($"{curSpeed:0.0}x");
            totalGlobalProgress += totalGlobalProgressStep;
        }

        globalPB.SetProgress(curGlobalProgress * 1f / totalGlobalProgress);
    }

    public void IncrSpeed()
    {
        curSpeed = Math.Min(curSpeed * speedScale, maxSpeed);
        foreach (var item in gm.marbles)
        {
            foreach (var marble in item.Value)
            {
                var m = marble.GetComponent<TestMarble>();
                m.SetSpeed(curSpeed);
            }
        }
    }


    IEnumerator IncrTeamProcess()
    {
        while (gm.marbles.Keys.Count > 1)
        {
            foreach (var item in gm.teamCount)
            {
                if (gm.cores.ContainsKey(item.Key))
                {
                    IncrTeamProcess(item.Key, Mathf.Pow(item.Value, 1.5f) * teamScale);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    public void IncrTeamProcess(int team, float p)
    {
        curTeamProgress[team] += p;
        if (curTeamProgress[team] >= totalTeamsProgress[team])
        {
            curTeamProgress[team] = 0;
            var count = MarbleFission(team);
            teamPB[team].SetText($"{count}");
            totalTeamsProgress[team] += totalTeamProgressStep;
        }

        teamPB[team].SetProgress(curTeamProgress[team] * 1f / totalTeamsProgress[team]);
    }

    public int MarbleFission(int team)
    {
        if (!gm.marbles.ContainsKey(team))
        {
            return 0;
        }

        var mars = gm.marbles[team];
        var count = mars.Count;
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(mars[i]);
            var tm = go.GetComponent<TestMarble>();
            tm.SetSpeed(curSpeed);
            mars.Add(go);
        }

        return mars.Count;
    }

    public void Setup(int num)
    {
        curSpeed = initSpeed;
        globalPB.SetText($"{curSpeed:0.0}x");
        for (int i = 0; i < num; i++)
        {
            var team = 6 + i;
            var go = Instantiate(pbPrefab, gm.canvas.transform);
            var rt = go.GetComponent<RectTransform>();
            rt.localPosition += Vector3.down * (rt.rect.height + 0) * i;
            var tpb = go.GetComponent<TestProgressBar>();
            tpb.SetText("2");
            tpb.SetColor(gm.data.teamMap[team].tileColor);
            curTeamProgress.Add(team, 0);
            totalTeamsProgress.Add(team, totalTeamProgress);
            teamPB.Add(team, tpb);
        }

        StartCoroutine(IncrTeamProcess());
    }
}