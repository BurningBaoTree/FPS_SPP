using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    protected CoolTimeSys cooltimer;

    public enum equipts
    {
        tools = 0,
        weapons
    }

    public equipts type;

    /// <summary>
    /// ������ �ٵ�
    /// </summary>
    public Rigidbody rig;

    /// <summary>
    /// �ڽ� �ݶ��̴�
    /// </summary>
    public BoxCollider col;

    /// <summary>
    /// ��� �̸�
    /// </summary>
    public string EquipterableName;

    /// <summary>
    /// ��� ����
    /// </summary>
    public string EquipterableEplonation;

    /// <summary>
    /// �ִ� �Ѿ�
    /// </summary>
    public int maxbullet;

    /// <summary>
    /// �Ѿ� ���¿� ������ ������ ����� ��������Ʈ
    /// </summary>
    public Action bulletReduced;

    /// <summary>
    /// ���� �Ѿ�
    /// </summary>
    public int bullet;
    public int Bullet
    {
        get { return bullet; 
        }
        set
        {
            if(bullet != value)
            {
                bullet = value;
                bulletReduced?.Invoke();
            }
        }
    }

    /// <summary>
    /// ������ Ȱ��ȭ�Ǵ� �Լ� ����
    /// </summary>
    public Action IsEquiped;

    /// <summary>
    /// ���� ������ Ȱ��ȭ�Ǵ� �Լ� ����
    /// </summary>
    public Action DisEquiped;

    /// <summary>
    /// �տ� ��������� ��������Ʈ
    /// </summary>
    public Action IsOnHold;

    /// <summary>
    /// �տ��� �����Ҷ� ��������Ʈ
    /// </summary>
    public Action DisOnHold;

    /// <summary>
    /// ��� ��ư ������ Ȱ��ȭ�Ǵ� �Լ� ����
    /// </summary>
    public Action UseDelegate;

    /// <summary>
    /// ��� ��ư�� ���� Ȱ��ȭ �Ǵ� �Լ� ����
    /// </summary>
    public Action StopDelegate;

    /// <summary>
    /// ������
    /// </summary>
    public Action ReAction;

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
        cooltimer = transform.GetComponent<CoolTimeSys>();
    }
    protected virtual void Start()
    {

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

    /// <summary>
    /// �÷��̾ ��������� ��񿡼� ����� �Լ�.
    /// </summary>
    private void playerEquiped()
    {
        rig.velocity = Vector3.zero;
        rig.isKinematic = true;
        col.enabled = false;
        this.transform.localPosition = equipPos;
        this.transform.localRotation = equipRot;
    }
    /// <summary>
    /// �÷��̾ ��� ���������� ��񿡼� ����� �Լ�.
    /// </summary>
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
