using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class Team
{
    public int index;
    public Color ballColor;
    public Color tileColor;
    public Color coreColor;

    public Team(int idx, Color[] colors)
    {
        index = idx;
        ballColor = colors[0];
        tileColor = colors[1];
        coreColor = colors[2];
    }
}

// Color Table
public class CTab
{
    public Dictionary<string, Color[]> T;
    private int _ballCIdx;
    private int _tileCIdx;
    private int _coreCIdx;

    public CTab(Dictionary<string, string[]> config)
    {
        T = new Dictionary<string, Color[]>();
        foreach (var key in config.Keys)
        {
            var tmp = new List<Color>();
            foreach (var value in config[key])
            {
                tmp.Add(HexToColor(value));
            }

            T[key] = tmp.ToArray();
        }
    }

    public void SetIndex(int ball, int tile, int core)
    {
        _ballCIdx = ball;
        _tileCIdx = tile;
        _coreCIdx = core;
    }

    public Color[] G(string name)
    {
        var c = T[name];
        return new Color[] {c[_ballCIdx], c[_tileCIdx], c[_coreCIdx]};
    }

    public static Color HexToColor(string hexString)
    {
        if (hexString.StartsWith("#", StringComparison.InvariantCulture))
        {
            hexString = hexString.Substring(1); // strip #
        }

        if (hexString.Length == 6)
        {
            hexString += "FF"; // add alpha if missing
        }

        var hex = Convert.ToUInt32(hexString, 16);
        var r = ((hex & 0xff000000) >> 0x18) / 255f;
        var g = ((hex & 0xff0000) >> 0x10) / 255f;
        var b = ((hex & 0xff00) >> 8) / 255f;
        var a = ((hex & 0xff)) / 255f;

        return new Color(r, g, b, a);
    }
}


public class TestData : MonoBehaviour
{
    public int ballCIdx = 5;
    public int tileCIdx = 7;
    public int coreCIdx = 9;
    public Team[] teams;

    public Dictionary<int, Team> teamMap;
    private CTab _cTab;
    private bool _setup = false;

    private void Awake()
    {
        Setup();
    }

    void LoadColorTable()
    {
        TextAsset targetFile = Resources.Load<TextAsset>("Colors");
        var colorTable = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(targetFile.text);
        _cTab = new CTab(colorTable);
        _cTab.SetIndex(ballCIdx, tileCIdx, coreCIdx);
        teams = new[]
        {
            new Team(6, _cTab.G("red")),
            new Team(7, _cTab.G("orange")),
            new Team(8, _cTab.G("yellow")),
            new Team(9, _cTab.G("green")),
            new Team(10, _cTab.G("cyan")),
            new Team(11, _cTab.G("blue")),
            new Team(12, _cTab.G("purple")),
            new Team(13, _cTab.G("pink")),
            new Team(14, _cTab.G("grey")),
        };
    }

    public void Setup()
    {
        if (_setup)
        {
            return;
        }
        LoadColorTable();
        teamMap = new Dictionary<int, Team>();
        foreach (var team in teams)
        {
            // Add index to team map
            teamMap[team.index] = team;
            // Ignore Layer Collision
            Physics2D.IgnoreLayerCollision(team.index, team.index);
        }

        _setup = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}