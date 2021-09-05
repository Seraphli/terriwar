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
    public static Color BallRed => HexToColor("#EC7063");
    public static Color TileRed => HexToColor("#E74C3C");


    public static Color BallOrange => HexToColor("#EB984E");
    public static Color TileOrange => HexToColor("#E67E22");
    
    public static Color BallYellow => HexToColor("#F4D03F");
    public static Color TileYellow => HexToColor("#F1C40F");

    public static Color BallGreen => HexToColor("#58D68D");
    public static Color TileGreen => HexToColor("#2ECC71");
    
    public static Color BallCyan => HexToColor("#48C9B0");
    public static Color TileCyan => HexToColor("#1ABC9C");
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
        new Team() {index = 9, ballColor = CTab.BallCyan, tileColor = CTab.TileCyan}
    };

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}