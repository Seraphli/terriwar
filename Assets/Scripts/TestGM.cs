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
    public int coreSize = 2;
    [ReadOnly] public int tileNum;

    // In range
    bool In(int a, int v, int b)
    {
        return v >= a && v < b;
    }

    int CircleCase(int i, int j, float radius, int range)
    {
        float factor = Mathf.Sqrt(2f) / 2f;
        int halfRange = range / 2;
        // 顺时针
        // 上
        if (In(-halfRange, i, halfRange) &&
            In(-halfRange, j - (int) radius, halfRange))
        {
            return 1;
        }

        if (In(-halfRange, i - (int) (radius * factor), halfRange) &&
            In(-halfRange, j - (int) (radius * factor), halfRange))
        {
            return 2;
        }

        // 右
        if (In(-halfRange, i - (int) radius, halfRange) &&
            In(-halfRange, j, halfRange))
        {
            return 3;
        }

        if (In(-halfRange, i - (int) (radius * factor), halfRange) &&
            In(-halfRange, j + (int) (radius * factor), halfRange))
        {
            return 4;
        }

        // 下
        if (In(-halfRange, i, halfRange) &&
            In(-halfRange, j + (int) radius, halfRange))
        {
            return 5;
        }

        if (In(-halfRange, i + (int) (radius * factor), halfRange) &&
            In(-halfRange, j + (int) (radius * factor), halfRange))
        {
            return 6;
        }

        // 左
        if (In(-halfRange, i + (int) radius, halfRange) &&
            In(-halfRange, j, halfRange))
        {
            return 7;
        }

        if (In(-halfRange, i + (int) (radius * factor), halfRange) &&
            In(-halfRange, j - (int) (radius * factor), halfRange))
        {
            return 8;
        }

        return 0;
    }

    void PlaceColor(GameObject go, int i, int j, int size)
    {
        int[] cases = {1, 2, 3, 4, 5, 6, 7, 8};
        var _case = CircleCase(i, j, size * 0.75f, baseSize);
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