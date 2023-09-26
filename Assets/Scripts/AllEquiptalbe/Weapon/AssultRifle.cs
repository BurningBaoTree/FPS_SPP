using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : WeaponBAse
{
    ParticleSystem par;

    protected override void Awake()
    {
        base.Awake();
        par = GetComponent<ParticleSystem>();
        weaponName = "어썰트 라이플";
        weaponIntroduce = "가장 보편적인 대화수단";
        equipPos = new Vector3(-0.004f, 0.082f, 0.051f);
        equipRot = Quaternion.Euler(-118.702f, 91.269f,14.657f);
        maxbullet = 34;
        bullet = maxbullet;
    }
    private void OnEnable()
    {
        UseDelegate += Fired;
        StopDelegate += stopFire;
    }
    private void OnDisable()
    {
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
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("착탄");
    }
}
