using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    // Color Palette
    public class Palette
    {
        private Dictionary<string, Color[]> P;

        public Palette(Dictionary<string, string[]> config)
        {
            P = new Dictionary<string, Color[]>();
            foreach (var key in config.Keys)
            {
                var tmp = new List<Color>();
                foreach (var value in config[key])
                {
                    tmp.Add(HexToColor(value));
                }

                P[key] = tmp.ToArray();
            }
        }

        public Color[] Get(string name)
        {
            return P[name];
        }

        public Color Get(string name, int index)
        {
            return P[name][index];
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
}