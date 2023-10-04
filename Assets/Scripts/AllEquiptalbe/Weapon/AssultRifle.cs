using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    ParticleSystem par;
    ParticleSystem.ShapeModule parshape;
    GameManager gameManager;
    PlayerMove playermove;

    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    int BulletCount
    {
        get
        {
            return collisionEvents.Count;
        }
        set
        {
            if (collisionEvents.Count < value)
            {
                BulletUse--;
                Debug.Log("파티클 갯수: " + BulletUse);
            }
        }
    }

    int BulletUse
    {
        get
        {
            return bullet;
        }
        set
        {
            if (bullet != value)
            {
                playermove.xis -= 1f;
                bullet = value;
                if (bullet == 0)
                {
                    fireAble = false;
                }
            }
        }
    }
    float angleer = 0;
    float maxAngle = 5f;
    float Angler
    {
        get
        {
            return angleer;
        }
        set
        {
            angleer = value;
            if (angleer > maxAngle)
            {
                angleer = maxAngle;
            }
        }
    }

    bool fireAble = true;


    protected override void Awake()
    {
        base.Awake();
        par = GetComponent<ParticleSystem>();
        parshape = par.shape;
        weaponName = "어썰트 라이플";
        weaponIntroduce = "가장 보편적인 대화수단";
        equipPos = new Vector3(-0.004f, 0.082f, 0.051f);
        equipRot = Quaternion.Euler(-118.702f, 91.269f, 14.657f);
        maxbullet = 34;
        bullet = maxbullet;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        UseDelegate += Fired;
        StopDelegate += stopFire;
    }
    protected override void Start()
    {
        gameManager = GameManager.Inst;
        base.Start();
        playermove = gameManager.Playermove;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StopDelegate -= stopFire;
        UseDelegate -= Fired;
    }

    void Fired()
    {
        par.Play();
        Updater += FireHandling;
    }
    void FireHandling()
    {
        parshape.angle = Angler;
        Angler += 0.5f;
    }
    void stopFire()
    {
        par.Stop();
        Updater -= FireHandling;
        parshape.angle = 0;
        Angler = 0;
    }
    void reLoad()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        int numCollisions = par.GetCollisionEvents(other, collisionEvents);

        foreach (ParticleCollisionEvent events in collisionEvents)
        {
            if (other.CompareTag("Wall") || other.CompareTag("Floor"))
            {
                GameObject heat = Instantiate(HeatEffect);
                heat.transform.position = events.intersection;
                heat.transform.rotation = Quaternion.LookRotation(events.normal);
            }
        }
    }
    /*    이후로 해야 할 일
            쏠때마다 총알 차감
            릴로딩*/
}
