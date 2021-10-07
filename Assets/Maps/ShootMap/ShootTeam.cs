using System.Collections;
using System.Collections.Generic;
using MarbleRaceBase.Define;
using MarbleRaceBase.Utility;
using TMPro;
using UnityEngine;
using Team = MarbleRaceBase.Define.Team;

public class ShootTeam : MonoBehaviour
{
    public GameObject fallBall;
    public GameObject cannon;
    public Color ballColor;

    public SlotsBase slots;
    public int index;

    public MarbleRaceBase.Define.Cannon c;

    public int tileCount;
    public TextMeshPro txtAmmo;
    public TextMeshPro txtCount;

    public float initTime = 15f;
    public float interval = 120f;

    private float curTime = 0f;
    private GM gm;
    private int ballCount = 0;

    public void Shoot()
    {
        c.isShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        c = cannon.GetComponent<MarbleRaceBase.Define.Cannon>();
        curTime = initTime;
        gm = MonoUtils.GetGM();
        gm.realTimeData.teams.Add(index,
            new MarbleRaceBase.Define.Team(slots.slots[index]));
        gm.realTimeData.teams[index].tileCount = 49;
    }

    void SpawnBall()
    {
        var ball = Instantiate(fallBall, new Vector3(0, 0), Quaternion.identity,
            transform);
        ball.transform.localPosition = new Vector3(0, 1.3f);
        var mNewForce = new Vector2(Random.Range(-10.0f, 10.0f),
            Random.Range(-10.0f, 10.0f));
        ball.GetComponent<Rigidbody2D>()
            .AddForce(mNewForce.normalized * 0.01f, ForceMode2D.Impulse);
        ball.GetComponent<SpriteRenderer>().color = ballColor;
        ballCount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        tileCount = gm.realTimeData.teams[index].tileCount;
        txtAmmo.text = $"{c.ammo}";
        txtCount.text = $"{tileCount}";
        if (ballCount < 5)
        {
            curTime -= Time.deltaTime;
            if (curTime < 0)
            {
                curTime = interval;
                SpawnBall();
            }
        }
    }
}