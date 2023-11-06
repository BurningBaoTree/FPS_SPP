using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    /// <summary>
    /// ��ƼŬ �ý��� (�Ѿ�)
    /// </summary>
    ParticleSystem par;

    /// <summary>
    /// ���� �Ŵ���
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// �÷��̾� �̵� ��ũ��Ʈ
    /// </summary>
    PlayerMove playermove;

    /// <summary>
    /// �ݵ� ����
    /// </summary>
    public Vector3 nuckup;

    /// <summary>
    /// �ٶ󺸴� ���� ����Ʈ ��ġ
    /// </summary>
    Vector3 def = new Vector3(0, 0, 50);

    /// <summary>
    /// �˹� ������
    /// </summary>
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

    /// <summary>
    /// ��ƼŬ�� ��ȣ�ۿ��ϴ� ���� �۾� ����Ʈ
    /// </summary>
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    /// <summary>
    /// ��� �� ���� üũ
    /// </summary>
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

    /// <summary>
    /// �Ѿ� ������Ƽ
    /// </summary>
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

    /// <summary>
    /// ��� �������� üũ
    /// </summary>
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

    /// <summary>
    /// ������ ������ üũ
    /// </summary>
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

    /// <summary>
    /// �ݵ� ȸ������ üũ
    /// </summary>
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
        gameManager = GameManager.Inst;
        base.Start();
        playermove = gameManager.Playermove;
        IsOnHold += EquipedAllert;
        DisOnHold += DisEquipedAllert;
        IsOnHold += EquiptInitialize;
        DisOnHold += EquiptDisInitialize;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
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
    void EquiptInitialize()
    {
        UseDelegate += Fired;
        StopDelegate += stopFire;
        ReAction += reLoad;
    }
    void EquiptDisInitialize()
    {
        ReAction -= reLoad;
        StopDelegate -= stopFire;
        UseDelegate -= Fired;
    }
    void EquipedAllert()
    {
        Updater += LoockAtDot;
    }
    void DisEquipedAllert()
    {
        Updater -= LoockAtDot;
    }
    void LoockAtDot()
    {
        playermove.dot.localPosition = nuckup;
    }
    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    void InitializeWeapon()
    {
        Bullet = maxbullet;
    }
    /// <summary>
    /// ��� �Լ�
    /// </summary>
    void Fired()
    {
        FireActive = true;
        headDotToZero(50);
    }

    /// <summary>
    /// �ݵ� �Լ�
    /// </summary>
    void FireHandling()
    {
        float x = UnityEngine.Random.Range(-innerNuckbackControl.x * nuckback, innerNuckbackControl.x * nuckback);
        float y = UnityEngine.Random.Range(0, innerNuckbackControl.y * nuckback);
        nuckup.x = x;
        nuckup.y = y;
        nuckup.z = 50;
    }

    /// <summary>
    /// ��� ������ ȣ�� �Լ�
    /// </summary>
    void stopFire()
    {
        FireActive = false;
        headDotToZero(50);
    }

    /// <summary>
    /// ������϶� updater���� ����Ǵ� �Լ�
    /// </summary>
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

    /// <summary>
    /// �ݵ��� ȸ���ϴ� �Լ�
    /// </summary>
    /// <param name="timespeed">ȸ�� ���ӵ�</param>
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

    /// <summary>
    /// �������� ȣ���ϴ� �Լ�
    /// </summary>
    void reLoad()
    {
        //���������� �ƴϰ� ������� �ƴҶ�
        if (!ReLoading && !FireActive)
        {
            nuckup = def;
            Nuckgage = 0;
            ReLoading = true;
        }
    }

    /// <summary>
    /// �������� �������� �����ϴ� �Լ�
    /// </summary>
    void isLoaded()
    {
        //1�� ��Ÿ�� ����� ������ ����
        if (cooltimer.coolclocks[1].coolEnd)
        {
            Bullet = maxbullet;
            FireAble = true;
            ReLoading = false;
            Updater -= isLoaded;
        }
    }

    /// <summary>
    /// ������ �ִϸ��̼� �Լ�
    /// </summary>
    void reloadAnimation()
    {
        nuckup = Vector3.MoveTowards(nuckup, -Vector3.up * 15, Time.deltaTime * 50);
        if (nuckup.y < -14 && cooltimer.coolclocks[1].coolEnd)
        {
            headDotToZero(50);
            Updater -= reloadAnimation;
        }
    }
}
