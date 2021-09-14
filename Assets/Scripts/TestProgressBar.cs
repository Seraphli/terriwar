using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestProgressBar : MonoBehaviour
{
    public Text text;
    public Image bar;
    public Image barBG;

    public void SetText(string t)
    {
        text.text = t;
    }

    public void SetColor(Color c)
    {
        bar.color = c;
    }

    public void SetProgress(float p)
    {
        var r = bar.rectTransform;
        r.sizeDelta = new Vector2(p * barBG.rectTransform.sizeDelta.x, r.sizeDelta.y);
    }
}