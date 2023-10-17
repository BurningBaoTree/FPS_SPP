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
    Vector3 nuckup;

    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    bool fireActive = false;
    bool FireActive
    {
        get
        {
            return fireActive;
        }
        set
        {
            if (fireActive != value)
            {
                fireActive = value;
                if (fireActive)
                {
                    Updater += FireUSe;
                }
                else
                {
                    Updater -= FireUSe;
                }
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
                playermove.xis -= nuckback;
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
    bool FireAble
    {
        get
        {
            return fireAble;
        }
        set
        {
            if (fireAble != value)
            {
                fireAble = value;
            }
        }
    }


    protected override void Awake()
    {
        base.Awake();
        par = GetComponent<ParticleSystem>();
        parshape = par.shape;
        equipPos = new Vector3(-0.004f, 0.082f, 0.051f);
        equipRot = Quaternion.Euler(-118.702f, 91.269f, 14.657f);
        InitializeWeapon();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        UseDelegate += Fired;
        StopDelegate += stopFire;
        Updater += () => { playermove.dot.localPosition = nuckup; };
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
    void InitializeWeapon()
    {
        bullet = maxbullet;
        weaponName = "어썰트 라이플";
        weaponIntroduce = "가장 보편적인 대화수단";
    }
    void Fired()
    {
        FireActive = true;
    }
    void FireHandling()
    {
        float x = UnityEngine.Random.Range(-1, 1.1f);
        float y = UnityEngine.Random.Range(-1, 1.1f);
        nuckup.x = x;
        nuckup.y = y;
        /*        parshape.angle = Angler;
                Angler += 0.5f;*/
    }
    void stopFire()
    {
        FireActive = false;
        parshape.angle = 0;
        Angler = 0;
        headDotToZero();
    }

    void FireUSe()
    {
        if (cooltimer.coolclocks[0].coolEnd)
        {
            par.Emit(1);
            FireHandling();
            cooltimer.CoolTimeStart(0, fireSpeed);
            BulletUse--;
        }
    }
    void headDotToZero()
    {
        nuckup = Vector3.MoveTowards(playermove.dot.position, Vector3.zero, 5 * Time.deltaTime);
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
