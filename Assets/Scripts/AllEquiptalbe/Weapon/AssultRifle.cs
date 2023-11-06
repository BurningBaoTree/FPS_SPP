using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    /// <summary>
    /// 파티클 시스템 (총알)
    /// </summary>
    ParticleSystem par;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// 플레이어 이동 스크립트
    /// </summary>
    PlayerMove playermove;

    /// <summary>
    /// 반동 벡터
    /// </summary>
    public Vector3 nuckup;

    /// <summary>
    /// 바라보는 지점 디폴트 위치
    /// </summary>
    Vector3 def = new Vector3(0, 0, 50);

    /// <summary>
    /// 넉백 게이지
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
    /// 파티클에 상호작용하는 물리 작업 리스트
    /// </summary>
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    /// <summary>
    /// 사격 중 인지 체크
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
    /// 총알 프로퍼티
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
    /// 사격 가능한지 체크
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
    /// 재장전 중인지 체크
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
    /// 반동 회복상태 체크
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
    /// 무기 초기화
    /// </summary>
    void InitializeWeapon()
    {
        Bullet = maxbullet;
    }
    /// <summary>
    /// 사격 함수
    /// </summary>
    void Fired()
    {
        FireActive = true;
        headDotToZero(50);
    }

    /// <summary>
    /// 반동 함수
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
    /// 사격 중지시 호출 함수
    /// </summary>
    void stopFire()
    {
        FireActive = false;
        headDotToZero(50);
    }

    /// <summary>
    /// 사격중일때 updater에서 실행되는 함수
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
    /// 반동을 회복하는 함수
    /// </summary>
    /// <param name="timespeed">회복 가속도</param>
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
    /// 재장전을 호출하는 함수
    /// </summary>
    void reLoad()
    {
        //재장전중이 아니고 사격중이 아닐때
        if (!ReLoading && !FireActive)
        {
            nuckup = def;
            Nuckgage = 0;
            ReLoading = true;
        }
    }

    /// <summary>
    /// 재장전이 끝났는지 감시하는 함수
    /// </summary>
    void isLoaded()
    {
        //1번 쿨타임 종료시 재장전 종료
        if (cooltimer.coolclocks[1].coolEnd)
        {
            Bullet = maxbullet;
            FireAble = true;
            ReLoading = false;
            Updater -= isLoaded;
        }
    }

    /// <summary>
    /// 재장전 애니메이션 함수
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
