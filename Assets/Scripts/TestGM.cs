using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class TestGM : MonoBehaviour
{
    public TestData data;
    public GameObject tile;
    public float tileSize;
    public float backgroundSize;
    public int baseSize = 4;
    [ReadOnly] public int tileNum;

    // In range
    bool In(int a, int v, int b)
    {
        return v >= a && v < b;
    }

    int CircleTileCase(int i, int j, float radius)
    {
        float factor = Mathf.Sqrt(2f) / 2f;
        // 顺时针
        // 上
        if (In(-baseSize / 2, i, baseSize / 2) &&
            In(-baseSize / 2, j - (int) radius, baseSize / 2))
        {
            return 1;
        }

        if (In(-baseSize / 2, i - (int) (radius * factor), baseSize / 2) &&
            In(-baseSize / 2, j - (int) (radius * factor), baseSize / 2))
        {
            return 2;
        }

        // 右
        if (In(-baseSize / 2, i - (int) radius, baseSize / 2) &&
            In(-baseSize / 2, j, baseSize / 2))
        {
            return 3;
        }

        if (In(-baseSize / 2, i - (int) (radius * factor), baseSize / 2) &&
            In(-baseSize / 2, j + (int) (radius * factor), baseSize / 2))
        {
            return 4;
        }

        // 下
        if (In(-baseSize / 2, i, baseSize / 2) &&
            In(-baseSize / 2, j + (int) radius, baseSize / 2))
        {
            return 5;
        }

        if (In(-baseSize / 2, i + (int) (radius * factor), baseSize / 2) &&
            In(-baseSize / 2, j + (int) (radius * factor), baseSize / 2))
        {
            return 6;
        }

        // 左
        if (In(-baseSize / 2, i + (int) radius, baseSize / 2) &&
            In(-baseSize / 2, j, baseSize / 2))
        {
            return 7;
        }

        if (In(-baseSize / 2, i + (int) (radius * factor), baseSize / 2) &&
            In(-baseSize / 2, j - (int) (radius * factor), baseSize / 2))
        {
            return 8;
        }

        return 0;
    }

    void PlaceColor(GameObject go, int i, int j, int size)
    {
        int[] cases = {1, 2, 3, 4, 5, 6, 7, 8};
        var _case = CircleTileCase(i, j, size * 0.75f);
        if (Array.IndexOf(cases, _case) > -1)
        {
            var sr = go.GetComponent<SpriteRenderer>();
            sr.color = data._teamMap[_case + 5].tileColor;
        }
    }

    void PlaceTiles()
    {
        var tileZ = tile.transform.position.z;
        tile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        var tiles = new GameObject("Tiles");
        var size = (int) Math.Floor((backgroundSize / 2) / tileSize) + 1;
        tileNum = size * 2;
        GameObject go;
        for (int i = -size; i < size; i++)
        {
            for (int j = -size; j < size; j++)
            {
                var pos = new Vector3((i + 0.5f) * tileSize, (j + 0.5f) * tileSize, tileZ);
                go = Instantiate(tile, pos, Quaternion.identity, tiles.transform);
                PlaceColor(go, i, j, size);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        data.Setup();
        PlaceTiles();
    }

    // Update is called once per frame
    void Update()
    {
    }
}