using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBAse : Equipments
{
    public GameObject HeatEffect;
    protected string weaponName;
    protected string weaponIntroduce;
    public float damage;
    public float fireSpeed;
    public float bulletSpeedf;
    public int maxbullet;
    public int bullet;
    protected override void Awake()
    {
        base.Awake();
    }

}
