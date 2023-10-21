using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    ParticleSystem par;
    GameManager gameManager;
    PlayerMove playermove;
    public Vector3 nuckup;
    Vector3 def = new Vector3(0, 0, 50);
    float nuckgage = 0;
    float Nuckgage
    {
        get
        {
            return nuckgage;
        }
        set
        {
            nuckgage = value;
            if (nuckgage > nuckbackMax)
            {
                nuckgage = nuckbackMax;
            }
            else if (nuckgage < 0)
            {
                nuckgage = 0;
            }
        }
    }

    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public bool fireActive = false;
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
                if (fireActive && !ReLoading)
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
            return Bullet;
        }
        set
        {
            if (Bullet != value)
            {
                Bullet = value;
                if (Bullet < 0)
                {
                    Bullet = 0;
                    FireAble = false;
                }
            }
        }
    }

    public bool fireAble = true;
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
    public bool reloading = false;
    bool ReLoading
    {
        get
        {
            return reloading;
        }
        set
        {
            if (reloading != value)
            {
                reloading = value;
                if (reloading)
                {
                    cooltimer.CoolTimeStart(1, 3);
                    Updater += reloadAnimation;
                    Updater += isLoaded;
                }
            }
        }
    }

    public bool recuvered = true;

    protected override void Awake()
    {
        base.Awake();
        par = GetComponent<ParticleSystem>();
        equipPos = new Vector3(-0.004f, 0.082f, 0.051f);
        equipRot = Quaternion.Euler(-118.702f, 91.269f, 14.657f);
        InitializeWeapon();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Start()
    {
        UseDelegate += Fired;
        StopDelegate += stopFire;
        ReAction += reLoad;
        Updater += () =>
        {
            playermove.dot.localPosition = nuckup;
        };
        gameManager = GameManager.Inst;
        base.Start();
        playermove = gameManager.Playermove;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        ReAction -= reLoad;
        StopDelegate -= stopFire;
        UseDelegate -= Fired;
    }
    void InitializeWeapon()
    {
        Bullet = maxbullet;
    }
    void Fired()
    {
        FireActive = true;
        headDotToZero(50);
    }
    void FireHandling()
    {
        float x = UnityEngine.Random.Range(-innerNuckbackControl.x * nuckback, innerNuckbackControl.x * nuckback);
        float y = UnityEngine.Random.Range(0, innerNuckbackControl.y * nuckback);
        nuckup.x = x;
        nuckup.y = y;
        nuckup.z = 50;
    }
    void stopFire()
    {
        FireActive = false;
        headDotToZero(50);
    }

    void FireUSe()
    {
        if (cooltimer.coolclocks[0].coolEnd && FireAble)
        {
            Nuckgage += nuckback;
            par.Emit(1);
            FireHandling();
            cooltimer.CoolTimeStart(0, fireRate);
            BulletUse--;
            playermove.xis -= Nuckgage;
        }
    }
    void headDotToZero(float timespeed)
    {
        Vector3 distance;
        void backto()
        {
            distance = nuckup - def;
            nuckup = Vector3.MoveTowards(nuckup, def, Time.deltaTime * timespeed);
            Nuckgage -= Time.deltaTime * reliseSpeed;
            if (ReLoading)
            {
                Updater -= backto;
            }
            if (distance.sqrMagnitude < 0.5f && Nuckgage < 0.1f)
            {
                Nuckgage = 0;
                nuckup = def;
                recuvered = true;
                Updater -= backto;
            }
        }
        if (recuvered)
        {
            Updater += backto;
            recuvered = false;
        }
    }
    void reLoad()
    {
        if (!ReLoading && !FireActive)
        {
            nuckup = def;
            Nuckgage = 0;
            ReLoading = true;
        }
    }
    void isLoaded()
    {
        if (cooltimer.coolclocks[1].coolEnd)
        {
            Bullet = maxbullet;
            FireAble = true;
            ReLoading = false;
            Updater -= isLoaded;
        }
    }
    void reloadAnimation()
    {
        nuckup = Vector3.MoveTowards(nuckup, -Vector3.up * 15, Time.deltaTime * 50);
        if (nuckup.y < -14 && cooltimer.coolclocks[1].coolEnd)
        {
            headDotToZero(50);
            Updater -= reloadAnimation;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        int numCollisions = par.GetCollisionEvents(other, collisionEvents);
        foreach (ParticleCollisionEvent events in collisionEvents)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyBase enemy = other.GetComponent<EnemyBase>();
                enemy.HP -= damage;
            }
            if (other.CompareTag("Wall") || other.CompareTag("Floor"))
            {
                GameObject heat = Instantiate(HeatEffect);
                heat.transform.position = events.intersection;
                heat.transform.rotation = Quaternion.LookRotation(events.normal);
            }
        }
    }
}
