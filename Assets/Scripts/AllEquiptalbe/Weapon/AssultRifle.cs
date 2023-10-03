using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    ParticleSystem par;
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
                if (bullet == 0)
                {
                    fireAble = false;
                }
            }
        }
    }
    bool fireAble = true;

    protected override void Awake()
    {
        base.Awake();
        par = GetComponent<ParticleSystem>();
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
        base.Start();

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        StopDelegate = stopFire;
        UseDelegate -= Fired;
    }
    void Fired()
    {
        par.Play();
    }
    void stopFire()
    {
        par.Stop();
    }
    void reLoad()
    {

    }
    private void OnParticleCollisionEvent(ParticleSystem particleSystem, ParticleCollisionEvent[] collisionEvents)
    {
        // 파티클 생성 이벤트가 발생할 때 호출되는 콜백 함수입니다.
        // 각 이벤트마다 파티클 갯수를 1씩 증가시킵니다.
        BulletUse -= collisionEvents.Length;

        // 원하는 동작을 수행할 수 있습니다.
        Debug.Log("파티클 갯수: " + BulletUse);
    }
    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
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
