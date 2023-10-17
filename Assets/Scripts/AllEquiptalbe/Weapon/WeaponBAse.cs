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
    /// ���� ���� �Ÿ�
    /// </summary>
    public Range rangeOfWeapon;

    /// <summary>
    /// ��ź�� ������
    /// </summary>
    public GameObject HeatEffect;

    /// <summary>
    /// ������
    /// </summary>
    public float damage;

    /// <summary>
    /// �߻� �ӵ�
    /// </summary>
    public float fireRate;

    /// <summary>
    /// �Ѿ� �ӵ�
    /// </summary>
    public float bulletSpeed;

    /// <summary>
    /// �ִ� �Ѿ�
    /// </summary>
    public int maxbullet;

    /// <summary>
    /// ���� �Ѿ�
    /// </summary>
    public int bullet;

    /// <summary>
    /// �ݵ�
    /// </summary>
    public float nuckback;

    /// <summary>
    /// ���� �ݵ�
    /// </summary>
    public float nuckbackMax;

    /// <summary>
    /// ��� �� ȸ�� �ӵ�
    /// </summary>
    public float reliseSpeed;



    protected override void Awake()
    {
        base.Awake();
    }
}
