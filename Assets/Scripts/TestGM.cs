using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestGM : MonoBehaviour
{
    public Team[] teams;
    public GameObject tile;
    private Dictionary<int, Team> _teamMap = new Dictionary<int, Team>();

    void AssignTeamMap()
    {
        foreach (var t in teams)
        {
            _teamMap.Add(t.index, t);
        }
    }

    void IgnoreCollide()
    {
        foreach (var item in _teamMap)
        {
            Physics2D.IgnoreLayerCollision(item.Key, item.Key);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        AssignTeamMap();
        IgnoreCollide();
    }

    // Update is called once per frame
    void Update()
    {
    }
}