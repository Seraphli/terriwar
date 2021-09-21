using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int team;
    public ShootGM gm;

    public GameObject marble;

    public SpriteRenderer[] srs;

    public void ChangeColor(Color c)
    {
        foreach (var sr in srs)
        {
            sr.color = c;
        }
    }
}