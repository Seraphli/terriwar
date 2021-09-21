using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class PaletteData : MonoBehaviour
    {
        public TextAsset config;
        public int ballCIdx = 5;
        public int tileCIdx = 7;
        public int coreCIdx = 9;
        public int defaultCIdx = 3;

        private Palette _palette;

        void LoadColorTable()
        {
            var colorConfig =
                JsonConvert.DeserializeObject<Dictionary<string, string[]>>(config.text);
            _palette = new Palette(colorConfig);
        }

        public Color[] Get(string name)
        {
            var c = _palette.Get(name);
            return new Color[] {c[ballCIdx], c[tileCIdx], c[coreCIdx]};
        }

        public Color GetDefault(string name)
        {
            return _palette.Get(name, defaultCIdx);
        }

        public void Setup()
        {
            LoadColorTable();
        }
    }
}