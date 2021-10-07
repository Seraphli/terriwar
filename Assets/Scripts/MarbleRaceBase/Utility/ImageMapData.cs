using MarbleRaceBase.Define;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class ImageMapData : MapData
    {
        public float tileSize = 0.2f;
        public float tileZ = 0.2f;
        public int paletteLine = 1;
        public Texture2D image;
        public Texture2D imageData;

        public Color32[] colors;
        public int[] commonData;
        public GameObject[] prefabs;
        public GameObject defaultPrefab;
    }
}