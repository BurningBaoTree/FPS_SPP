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
        weaponName = "���Ʈ ������";
        weaponIntroduce = "���� �������� ��ȭ����";
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
        // ��ƼŬ ���� �̺�Ʈ�� �߻��� �� ȣ��Ǵ� �ݹ� �Լ��Դϴ�.
        // �� �̺�Ʈ���� ��ƼŬ ������ 1�� ������ŵ�ϴ�.
        BulletUse -= collisionEvents.Length;

        // ���ϴ� ������ ������ �� �ֽ��ϴ�.
        Debug.Log("��ƼŬ ����: " + BulletUse);
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
/*    ���ķ� �ؾ� �� ��
        �򶧸��� �Ѿ� ����
        ���ε�*/
}
