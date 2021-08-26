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
    public GameObject circle;

    public Transform hexNode;
    public Transform squareNode;

    // private float _threshold = 0.01f;
    private Dictionary<int, Color> _colorMap;

    private float _hexZ;
    private float _squareZ;

    void Init()
    {
        _hexZ = hex.transform.position.z;
        _squareZ = square.transform.position.z;
    }

    // void OutBound()
    // {
    //     float r = hexRadius * (float) Math.Sqrt(3) / 4.0f;
    //     var go = Instantiate(hex, new Vector3(0, 2.0f * r, _hexZ), Quaternion.identity);
    //     go.transform.parent = hexNode;
    //     go = Instantiate(hex, new Vector3(0, -2.0f * r, _hexZ), Quaternion.identity);
    //     go.transform.parent = hexNode;
    //
    //     go = Instantiate(hex, new Vector3(hexRadius / 4 * 3, r, _hexZ), Quaternion.identity);
    //     go.transform.parent = hexNode;
    //     go = Instantiate(hex, new Vector3(hexRadius / 4 * 3, -r, _hexZ), Quaternion.identity);
    //     go.transform.parent = hexNode;
    //
    //     go = Instantiate(hex, new Vector3(-hexRadius / 4 * 3, r, _hexZ), Quaternion.identity);
    //     go.transform.parent = hexNode;
    //     go = Instantiate(hex, new Vector3(-hexRadius / 4 * 3, -r, _hexZ), Quaternion.identity);
    //     go.transform.parent = hexNode;
    // }

    void OutBound()
    {
        var go = Instantiate(hex, new Vector3(0, hexRadius, _hexZ), Quaternion.identity);
        go.transform.parent = hexNode;
        go = Instantiate(hex, new Vector3(0, -hexRadius, _hexZ), Quaternion.identity);
        go.transform.parent = hexNode;

        go = Instantiate(hex, new Vector3(hexRadius, 0, _hexZ), Quaternion.identity);
        go.transform.parent = hexNode;
        go = Instantiate(hex, new Vector3(-hexRadius, 0, _hexZ), Quaternion.identity);
        go.transform.parent = hexNode;
    }

    void ColorMap()
    {
        _colorMap = new Dictionary<int, Color>();
        _colorMap.Add(6, Color.red);
        _colorMap.Add(7, new Color(1f, 165 / 255.0f, 0));
        _colorMap.Add(8, Color.yellow);
        _colorMap.Add(9, Color.green);
        _colorMap.Add(10, Color.cyan);
        _colorMap.Add(11, Color.blue);
        _colorMap.Add(12, new Color(153 / 255.0f, 51 / 255.0f, 1.0f));
        _colorMap.Add(13, Color.magenta);
        _colorMap.Add(14, Color.gray);
    }

    void IgnoreCol()
    {
        foreach (var item in _colorMap)
        {
            Physics2D.IgnoreLayerCollision(item.Key, item.Key);
        }
    }

    GameObject CreateSquare(Vector3 pos)
    {
        var go = Instantiate(square, pos, Quaternion.identity);
        go.transform.localScale = new Vector3(squareX, squareX, squareX);
        go.transform.parent = squareNode;
        return go;
    }

    void PlaceSquare()
    {
        int initSize = 4;
        var size = (int) Math.Floor((hexRadius / 2) / squareX) + 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var go = CreateSquare(new Vector3((i + 0.5f) * squareX, (j + 0.5f) * squareX, _squareZ));
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = _colorMap[6];
                }

                go = CreateSquare(new Vector3(-(i + 0.5f) * squareX, (j + 0.5f) * squareX, _squareZ));
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = _colorMap[7];
                }

                go = CreateSquare(new Vector3((i + 0.5f) * squareX, -(j + 0.5f) * squareX, _squareZ));
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = _colorMap[8];
                }

                go = CreateSquare(new Vector3(-(i + 0.5f) * squareX, -(j + 0.5f) * squareX, _squareZ));
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = _colorMap[9];
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
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