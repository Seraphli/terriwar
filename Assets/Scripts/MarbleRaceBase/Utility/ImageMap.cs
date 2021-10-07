using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using MarbleRaceBase.Define;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class ImageMap : Map
    {
        private ImageMapData _mapData;
        public bool preview;
        public bool useImageColor;
        public bool refresh;

        private Dictionary<Color32, GameObject> _colorPrefabMap;
        private Dictionary<Color32, Slot> _colorSlotMap;
        private Dictionary<Color32, int> _colorDataMap;

        private string _groupName;

        private static int[] Color32ArrayToIntArray(Color32[] colors)
        {
            if (colors == null || colors.Length == 0)
                return null;

            int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));
            int length = lengthOfColor32 * colors.Length / Marshal.SizeOf(typeof(int));
            int[] ints = new int[length];

            GCHandle handle = default(GCHandle);
            try
            {
                handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
                IntPtr ptr = handle.AddrOfPinnedObject();
                Marshal.Copy(ptr, ints, 0, length);
            }
            finally
            {
                if (handle != default(GCHandle))
                    handle.Free();
            }

            return ints;
        }

        private void GetColors()
        {
            if (_mapData.image && _mapData.imageData)
            {
                List<Color32> colors = new List<Color32>();
                List<int> commonData = new List<int>();
                var imgColors = _mapData.image.GetPixels32();
                var imgData = _mapData.imageData.GetPixels32();
                var data = Color32ArrayToIntArray(imgData);
                for (int i = 0; i < _mapData.paletteLine; i++)
                {
                    for (int j = 0; j < _mapData.image.width; j++)
                    {
                        var c = imgColors[i * _mapData.image.width + j];
                        if (c.a == 0)
                        {
                            break;
                        }

                        colors.Add(c);
                        commonData.Add(data[i * _mapData.image.width + j]);
                    }
                }

                _mapData.colors = colors.ToArray();
                _mapData.commonData = commonData.ToArray();
            }
            else
            {
                _mapData.colors = new Color32[] { };
                _mapData.commonData = new int[] { };
            }
        }

        private void BuildDict()
        {
            _colorPrefabMap = new Dictionary<Color32, GameObject>();
            _colorSlotMap = new Dictionary<Color32, Slot>();
            _colorDataMap = new Dictionary<Color32, int>();
            int index;
            for (int i = 0; i < _mapData.colors.Length; i++)
            {
                try
                {
                    index = (_mapData.commonData[i] & 0xF0) >> 4;
                    _colorPrefabMap.Add(_mapData.colors[i], _mapData.prefabs[index]);
                }
                catch
                {
                    print($"default prefab");
                    _colorPrefabMap.Add(_mapData.colors[i], _mapData.defaultPrefab);
                }

                try
                {
                    index = _mapData.commonData[i] & 0xF;
                    _colorSlotMap.Add(_mapData.colors[i],
                        _mapData.slotsBase.slots[index]);
                }
                catch
                {
                    print($"default slot");
                    print($"{i} {_mapData.colors.Length} {_mapData.slotsBase.slots.Length}");
                    _colorSlotMap.Add(_mapData.colors[i], _mapData.slotsBase.slots[0]);
                }

                try
                {
                    _colorDataMap.Add(_mapData.colors[i], _mapData.commonData[i]);
                }
                catch
                {
                    print($"default data");
                    _colorDataMap.Add(_mapData.colors[i], 0);
                }
            }
        }

        public void PlaceTiles()
        {
            BuildDict();
            var imgColors = _mapData.image.GetPixels32();
            var imgData = _mapData.imageData.GetPixels32();
            var data = Color32ArrayToIntArray(imgData);
            int halfWidth = _mapData.image.width / 2;
            int halfHeight = _mapData.image.height / 2;
            var posIter = new LoopPos(halfWidth, halfHeight);
            _groupName = name + "Objs";
            // Clean up
            var g = GameObject.Find(_groupName);
            var objects = Resources.FindObjectsOfTypeAll<GameObject>()
                .Where(obj => obj.name == _groupName);
            foreach (var obj in objects)
            {
                MonoUtils.SafeDestroy(obj);
            }

            var tiles = new GameObject(_groupName);
            GameObject go;
            foreach (ValueTuple<float, float> pos in posIter)
            {
                var i = pos.Item1;
                var j = pos.Item2;
                int index = (int) ((i - 0.5f + halfWidth) +
                                   (j - 0.5f + halfHeight + _mapData.paletteLine) *
                                   _mapData.image.width);
                var realPos = new Vector3(i * _mapData.tileSize, j * _mapData.tileSize,
                    _mapData.tileZ);
                var c = imgColors[index];
                if (c.a == 0)
                {
                    continue;
                }

                var prefab = _colorPrefabMap[imgColors[index]];
                go = Instantiate(prefab, realPos, Quaternion.identity,
                    tiles.transform);
                var t = go.GetComponent<Element>();
                if (Application.isEditor)
                {
                    t.SetMaskInter(SpriteMaskInteraction.None);
                }

                if (useImageColor)
                {
                    t.SetColor(imgColors[index]);
                }

                t.SetPos(i - 0.5f, j - 0.5f);
                t.SetRealPos(realPos);
                t.SetSlot(_colorSlotMap[imgColors[index]]);
                t.SetParams(_colorDataMap[imgColors[index]], data[index]);
            }
        }

        public override void Setup()
        {
            _mapData = GetComponent<ImageMapData>();
            GetColors();
            PlaceTiles();
        }

        public void EditorSetup()
        {
            if (!refresh)
            {
                return;
            }
            _mapData = GetComponent<ImageMapData>();
            GetColors();
            if (preview)
            {
                PlaceTiles();
            }
        }
    }
}