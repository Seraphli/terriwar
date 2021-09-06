using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Team
{
    public int index;
    public Color ballColor;
    public Color tileColor;
}

// Color Table
public struct CTab
{
    public static Color BallRed => HexToColor("#EF5350");
    public static Color TileRed => HexToColor("#E53935");

    public static Color BallOrange => HexToColor("#FFA726");
    public static Color TileOrange => HexToColor("#FB8C00");

    public static Color BallYellow => HexToColor("#FFEE58");
    public static Color TileYellow => HexToColor("#FDD835");

    public static Color BallGreen => HexToColor("#66BB6A");
    public static Color TileGreen => HexToColor("#43A047");

    public static Color BallCyan => HexToColor("#26C6DA");
    public static Color TileCyan => HexToColor("#00ACC1");

    public static Color BallBlue => HexToColor("#29B6F6");
    public static Color TileBlue => HexToColor("#039BE5");

    public static Color BallPurple => HexToColor("#7E57C2");
    public static Color TilePurple => HexToColor("#5E35B1");

    public static Color BallMagenta => HexToColor("#AB47BC");
    public static Color TileMagenta => HexToColor("#8E24AA");

    public static Color BallPink => HexToColor("#EC407A");
    public static Color TilePink => HexToColor("#D81B60");

    public static Color BallIndigo => HexToColor("#5C6BC0");
    public static Color TileIndigo => HexToColor("#3949AB");

    public static Color BallBrown => HexToColor("#8D6E63");
    public static Color TileBrown => HexToColor("#6D4C41");

    public static Color BallGrey => HexToColor("#BDBDBD");
    public static Color TileGrey => HexToColor("#757575");

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
    public Team[] teams = new[]
    {
        new Team() {index = 6, ballColor = CTab.BallRed, tileColor = CTab.TileRed},
        new Team() {index = 7, ballColor = CTab.BallOrange, tileColor = CTab.TileOrange},
        new Team() {index = 8, ballColor = CTab.BallYellow, tileColor = CTab.TileYellow},
        new Team() {index = 9, ballColor = CTab.BallGreen, tileColor = CTab.TileGreen},
        new Team() {index = 10, ballColor = CTab.BallCyan, tileColor = CTab.TileCyan},
        new Team() {index = 11, ballColor = CTab.BallBlue, tileColor = CTab.TileBlue},
        new Team() {index = 12, ballColor = CTab.BallPurple, tileColor = CTab.TilePurple},
        new Team() {index = 13, ballColor = CTab.BallPink, tileColor = CTab.TilePink},
        new Team() {index = 14, ballColor = CTab.BallGrey, tileColor = CTab.TileGrey},
    };

    public Dictionary<int, Team> _teamMap;

    // Start is called before the first frame update
    void Awake()
    {
        _teamMap = new Dictionary<int, Team>();
        foreach (var team in teams)
        {
            // Add index to team map
            _teamMap[team.index] = team;
            // Ignore Layer Collision
            Physics2D.IgnoreLayerCollision(team.index, team.index);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}