using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankLine : MonoBehaviour
{
    public Image colorImage;
    public Text rankText;
    public Text numText;
    public int rank;
    public int num;

    public void SetColor(Color c)
    {
        colorImage.color = c;
    }
    
    public void SetRank(int r)
    {
        rank = r;
        rankText.text = $"{r}";
    }

    public void SetNum(int n)
    {
        num = n;
        numText.text = $"{n}";
    }
}