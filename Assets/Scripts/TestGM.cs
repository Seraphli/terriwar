using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Team
{
    public int index;
    public Color ballColor;
    public Color tileColor;
}

public class TestGM : MonoBehaviour
{
    public Team[] teams;
    public GameObject tile;
    private Dictionary<int, Team> _colorMap = new Dictionary<int, Team>();
    
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
