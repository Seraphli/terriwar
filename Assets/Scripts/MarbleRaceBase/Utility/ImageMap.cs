using System;
using System.Collections.Generic;
using System.Linq;
using MarbleRaceBase.Define;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class ImageMap : Map
    {
        public ImageMapData mapData;
        public bool preview;
        public bool useImageColor;

        private Dictionary<Color32, GameObject> _colorPrefabMap;
        private Dictionary<Color32, int> _colorSlotMap;

        private void GetColors()
        {
            if (mapData.image)
            {
                List<Color32> colors32 = new List<Color32>();
                var imgColors = mapData.image.GetPixels32();
                for (int i = 0; i < mapData.paletteLine; i++)
                {
                    for (int j = 0; j < mapData.image.width; j++)
                    {
                        var c = imgColors[i * mapData.image.width + j];
                        if (c.a == 0)
                        {
                            break;
                        }

                        colors32.Add(c);
                    }
                }

                mapData.colors = colors32.ToArray();
            }
            else
            {
                mapData.colors = new Color32[] { };
            }
        }

        private void BuildDict()
        {
            _colorPrefabMap = new Dictionary<Color32, GameObject>();
            _colorSlotMap = new Dictionary<Color32, int>();
            for (int i = 0; i < mapData.colors.Length; i++)
            {
                try
                {
                    _colorPrefabMap.Add(mapData.colors[i], mapData.prefabs[i]);
                    _colorSlotMap.Add(mapData.colors[i], mapData.slotsIndex[i]);
                }
                catch
                {
                    _colorPrefabMap.Add(mapData.colors[i], mapData.defaultPrefab);
                    _colorSlotMap.Add(mapData.colors[i], 0);
                }
            }
        }

        public override void Setup()
        {
            BuildDict();
            var imgColors = mapData.image.GetPixels32();
            int halfWidth = mapData.image.width / 2;
            int halfHeight = mapData.image.height / 2;
            var posIter = new LoopPos(halfWidth, halfHeight);
            if (Application.isEditor)
            {
                // Clean up
                var g = GameObject.Find("Tiles");
                var objects = Resources.FindObjectsOfTypeAll<GameObject>()
                    .Where(obj => obj.name == "Tiles");
                foreach (var obj in objects)
                {
                    MonoUtils.SafeDestroy(obj);
                }
            }

            var tiles = new GameObject("Tiles");
            GameObject go;
            foreach (ValueTuple<float, float> pos in posIter)
            {
                var i = pos.Item1;
                var j = pos.Item2;
                int index = (int) ((i - 0.5f + halfWidth) +
                                   (j - 0.5f + halfHeight + mapData.paletteLine) *
                                   mapData.image.width);
                var realPos = new Vector3(i * mapData.tileSize, j * mapData.tileSize,
                    mapData.tileZ);
                var prefab = _colorPrefabMap[imgColors[index]];
                go = Instantiate(prefab, realPos, Quaternion.identity,
                    tiles.transform);
                var t = go.GetComponent<Tile>();
                if (Application.isEditor)
                {
                    t.SetMaskInter(SpriteMaskInteraction.None);
                }

                if (useImageColor)
                {
                    t.SetColor(imgColors[index]);
                }

                t.SetPos(i, j);
            }
        }

        public void EditorSetup()
        {
            GetColors();
            if (preview)
            {
                Setup();
            }
        }
    }
}