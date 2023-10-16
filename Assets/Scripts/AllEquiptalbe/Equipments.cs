using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    public CoolTimeSys cooltimer;


    public Rigidbody rig;
    public BoxCollider col;

    /// <summary>
    /// ������ Ȱ��ȭ�Ǵ� �Լ� ����
    /// </summary>
    public Action IsEquiped;

    /// <summary>
    /// ���� ������ Ȱ��ȭ�Ǵ� �Լ� ����
    /// </summary>
    public Action DisEquiped;

    /// <summary>
    /// ��� ��ư ������ Ȱ��ȭ�Ǵ� �Լ� ����
    /// </summary>
    public Action UseDelegate;

    /// <summary>
    /// ��� ��ư�� ���� Ȱ��ȭ �Ǵ� �Լ� ����
    /// </summary>
    public Action StopDelegate;

    /// <summary>
    /// ������Ʈ�� �Լ� ��������Ʈ
    /// </summary>
    protected Action Updater;

    protected Vector3 equipPos;
    protected Quaternion equipRot;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        Updater = noNull;
    }
    protected virtual void OnEnable()
    {
        IsEquiped += playerEquiped;
        DisEquiped += playerDisEquiped;
        cooltimer = new CoolTimeSys();
    }
    protected virtual void Start()
    {
        IsEquiped += playerEquiped;
        DisEquiped += playerDisEquiped;
    }
    protected virtual void OnDisable()
    {
        DisEquiped -= playerDisEquiped;
        IsEquiped -= playerEquiped;
    }
    protected virtual void Update()
    {
        Updater();
    }
    private void playerEquiped()
    {
        rig.velocity = Vector3.zero;
        rig.isKinematic = true;
        col.enabled = false;
        this.transform.localPosition = equipPos;
        this.transform.localRotation = equipRot;
    }
    private void playerDisEquiped()
    {
        rig.isKinematic = false;
        col.enabled = true;
        rig.AddForce(Vector3.forward, ForceMode.Impulse);
    }
    protected void noNull()
    {

    }

}
