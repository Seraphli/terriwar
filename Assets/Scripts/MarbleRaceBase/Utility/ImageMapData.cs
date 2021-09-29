using MarbleRaceBase.Define;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class ImageMapData : MapData
    {
        public float tileSize = 0.2f;
        public float tileZ = 0.2f;
        public Texture2D image;

        public Color32[] colors;
        public GameObject[] prefabs;
        public GameObject defaultPrefab;
        public int[] slotsIndex;
 
    }
}