using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SceneGM : MonoBehaviour
{
    public float hexRadius = 10.0f;
    public float squareX = 0.2f;

    public GameObject hex;
    public GameObject square;
    public GameObject circle;

    public Transform hexNode;
    public Transform squareNode;

    // private float _threshold = 0.01f;
    public Dictionary<int, Color> colorMap;

    private float _hexZ;
    private float _squareZ;
    private float _circleZ;

    void Init()
    {
        _hexZ = hex.transform.position.z;
        _squareZ = square.transform.position.z;
        _circleZ = circle.transform.position.z;
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

    GameObject CreateSquare(Vector3 pos)
    {
        var go = Instantiate(square, pos, Quaternion.identity);
        go.transform.localScale = new Vector3(squareX, squareX, squareX);
        go.transform.parent = squareNode;
        return go;
    }

    void PlaceSquare()
    {
        int initSize = 3;
        var size = (int) Math.Floor((hexRadius / 2) / squareX) + 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var pos = new Vector3((i + 0.5f) * squareX, (j + 0.5f) * squareX, _squareZ);
                var go = CreateSquare(pos);
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = colorMap[6];
                    if (i - size / 2 == 0 && j - size / 2 == 0)
                    {
                        pos = new Vector3((i + 0.5f) * squareX, (j + 0.5f) * squareX, _circleZ);
                        var c = Instantiate(circle, pos, Quaternion.identity);
                        var b = c.GetComponent<Ball>();
                        b.type = 6;
                        b.sceneGm = this;
                        c.GetComponent<SpriteRenderer>().color = colorMap[6];
                        c.layer = 6;
                    }
                }

                pos = new Vector3(-(i + 0.5f) * squareX, (j + 0.5f) * squareX, _squareZ);
                go = CreateSquare(pos);
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = colorMap[7];
                    if (i - size / 2 == 0 && j - size / 2 == 0)
                    {
                        pos = new Vector3(-(i + 0.5f) * squareX, (j + 0.5f) * squareX, _circleZ);
                        var c = Instantiate(circle, pos, Quaternion.identity);
                        var b = c.GetComponent<Ball>();
                        b.type = 7;
                        b.sceneGm = this;
                        c.GetComponent<SpriteRenderer>().color = colorMap[7];
                        c.layer = 7;
                    }
                }

                pos = new Vector3((i + 0.5f) * squareX, -(j + 0.5f) * squareX, _squareZ);
                go = CreateSquare(pos);
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = colorMap[8];
                    if (i - size / 2 == 0 && j - size / 2 == 0)
                    {
                        pos = new Vector3((i + 0.5f) * squareX, -(j + 0.5f) * squareX, _circleZ);
                        var c = Instantiate(circle, pos, Quaternion.identity);
                        var b = c.GetComponent<Ball>();
                        b.type = 8;
                        b.sceneGm = this;
                        c.GetComponent<SpriteRenderer>().color = colorMap[8];
                        c.layer = 8;
                    }
                }

                pos = new Vector3(-(i + 0.5f) * squareX, -(j + 0.5f) * squareX, _squareZ);
                go = CreateSquare(pos);
                if (i - size / 2 <= initSize && i - size / 2 >= 0 && j - size / 2 <= initSize && j - size / 2 >= 0)
                {
                    go.GetComponent<SpriteRenderer>().color = colorMap[9];
                    if (i - size / 2 == 0 && j - size / 2 == 0)
                    {
                        pos = new Vector3(-(i + 0.5f) * squareX, -(j + 0.5f) * squareX, _circleZ);
                        var c = Instantiate(circle, pos, Quaternion.identity);
                        var b = c.GetComponent<Ball>();
                        b.type = 9;
                        b.sceneGm = this;
                        b.debug = true;
                        c.GetComponent<SpriteRenderer>().color = colorMap[9];
                        c.layer = 9;
                    }
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