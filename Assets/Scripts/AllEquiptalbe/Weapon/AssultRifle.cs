using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    ParticleSystem par;
    GameManager gameManager;
    PlayerMove playermove;
    Vector3 nuckup;
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
                bullet = value;
                if (bullet < 0)
                {
                    bullet = 0;
                    FireAble = false;
                }
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
    bool recuvered = true;

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
        UseDelegate += Fired;
        StopDelegate += stopFire;
        ReAction += reLoad;
        Updater += () =>
        {
            playermove.dot.localPosition = nuckup;
        };
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
    }
    void Fired()
    {
        FireActive = true;
    }
    void FireHandling()
    {
        float x = UnityEngine.Random.Range(-Nuckgage, Nuckgage);
        float y = UnityEngine.Random.Range(-Nuckgage, Nuckgage);
        nuckup.x = x;
        nuckup.y = y;
        nuckup.z = 50;
        /*        parshape.angle = Angler;
                Angler += 0.5f;*/
    }
    void stopFire()
    {
        FireActive = false;
        headDotToZero();
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
    void headDotToZero()
    {
        Vector3 def = new Vector3(0, 0, 50);
        void backto()
        {
            if (recuvered)
            {
                nuckup = Vector3.MoveTowards(nuckup, def, Time.deltaTime);
                Nuckgage -= Time.deltaTime * reliseSpeed;
                if (nuckup.sqrMagnitude < 0.5f && Nuckgage == 0)
                {
                    Debug.Log("회복 완료");
                    Updater -= backto;
                    recuvered = true;
                }
            }
        }
        Updater += backto;
        recuvered = false;
    }
    void reLoad()
    {
        playermove.animator.SetTrigger("ReLoad");
        bullet = maxbullet;
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
