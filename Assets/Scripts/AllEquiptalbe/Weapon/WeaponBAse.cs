using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBAse : Equipments
{
    public GameObject HeatEffect;
    public string weaponName;
    public string weaponIntroduce;
    public float damage;
    public float fireSpeed;
    public float bulletSpeedf;
    public int maxbullet;
    public int bullet;
    public float nuckback;
    protected override void Awake()
    {
        base.Awake();
    }
}
