using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBAse : Equipments
{
    public enum Range
    {
        front = 0,
        farRange
    }
    /// <summary>
    /// 무기 목적 거리
    /// </summary>
    public Range rangeOfWeapon;

    /// <summary>
    /// 피탄용 프리펩
    /// </summary>
    public GameObject HeatEffect;

    /// <summary>
    /// 데미지
    /// </summary>
    public float damage;

    /// <summary>
    /// 발사 속도
    /// </summary>
    public float fireRate;

    /// <summary>
    /// 총알 속도
    /// </summary>
    public float bulletSpeed;

    /// <summary>
    /// 최대 총알
    /// </summary>
    public int maxbullet;

    /// <summary>
    /// 현재 총알
    /// </summary>
    public int bullet;

    /// <summary>
    /// 반동
    /// </summary>
    public float nuckback;

    /// <summary>
    /// 쵀대 반동
    /// </summary>
    public float nuckbackMax;

    /// <summary>
    /// 사격 후 회복 속도
    /// </summary>
    public float reliseSpeed;



    protected override void Awake()
    {
        base.Awake();
    }
}
