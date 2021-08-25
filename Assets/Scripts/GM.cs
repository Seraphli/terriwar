using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GM : MonoBehaviour
{
    public float hexRadius = 10.0f;
    public float squareX = 0.2f;

    public GameObject hex;
    public GameObject square;

    // private float _threshold = 0.01f;
    private Dictionary<int, Color> colorMap;

    void OutBound()
    {
        float r = hexRadius * (float) Math.Sqrt(3) / 4.0f;
        Instantiate(hex, new Vector3(0, 2.0f * r, 0), Quaternion.identity);
        Instantiate(hex, new Vector3(0, -2.0f * r, 0), Quaternion.identity);

        Instantiate(hex, new Vector3(hexRadius / 4 * 3, r, 0), Quaternion.identity);
        Instantiate(hex, new Vector3(hexRadius / 4 * 3, -r, 0), Quaternion.identity);

        Instantiate(hex, new Vector3(-hexRadius / 4 * 3, r, 0), Quaternion.identity);
        Instantiate(hex, new Vector3(-hexRadius / 4 * 3, -r, 0), Quaternion.identity);
    }

    void ColorMap()
    {
        colorMap = new Dictionary<int, Color>();
        colorMap.Add(6, Color.red);
        colorMap.Add(7, new Color(1f, 165 / 255.0f, 0));
        colorMap.Add(8, Color.yellow);
        colorMap.Add(9, Color.green);
        colorMap.Add(10, Color.cyan);
        colorMap.Add(11, Color.blue);
        colorMap.Add(12, new Color(153 / 255.0f, 51 / 255.0f, 1.0f));
        colorMap.Add(13, Color.magenta);
        colorMap.Add(14, Color.gray);
    }

    void IgnoreCol()
    {
        foreach (var item in colorMap)
        {
            Physics2D.IgnoreLayerCollision(item.Key, item.Key);
        }
    }

    void PlaceSquare()
    {
        var size = (int) Math.Floor((hexRadius / 2) / squareX) + 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var go = Instantiate(square, new Vector3((i + 0.5f) * squareX, (j + 0.5f) * squareX, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(squareX, squareX, squareX);
                go = Instantiate(square, new Vector3(-(i + 0.5f) * squareX, (j + 0.5f) * squareX, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(squareX, squareX, squareX);
                go = Instantiate(square, new Vector3((i + 0.5f) * squareX, -(j + 0.5f) * squareX, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(squareX, squareX, squareX);
                go = Instantiate(square, new Vector3(-(i + 0.5f) * squareX, -(j + 0.5f) * squareX, 0), Quaternion.identity);
                go.transform.localScale = new Vector3(squareX, squareX, squareX);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ColorMap();
        PlaceSquare();
        OutBound();
        IgnoreCol();
    }

    // Update is called once per frame
    void Update()
    {
    }
}