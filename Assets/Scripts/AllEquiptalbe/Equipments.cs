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
    /// 리지디 바디
    /// </summary>
    public Rigidbody rig;

    /// <summary>
    /// 박스 콜라이더
    /// </summary>
    public BoxCollider col;

    /// <summary>
    /// 장비 이름
    /// </summary>
    public string EquipterableName;

    /// <summary>
    /// 장비 설명
    /// </summary>
    public string EquipterableEplonation;

    /// <summary>
    /// 최대 총알
    /// </summary>
    public int maxbullet;

    /// <summary>
    /// 총알 상태에 변동이 왔을때 실행될 델리게이트
    /// </summary>
    public Action bulletReduced;

    /// <summary>
    /// 현재 총알
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
    /// 장착시 활성화되는 함수 모음
    /// </summary>
    public Action IsEquiped;

    /// <summary>
    /// 장착 해제시 활성화되는 함수 모음
    /// </summary>
    public Action DisEquiped;

    /// <summary>
    /// 손에 쥐고있을때 델리게이트
    /// </summary>
    public Action IsOnHold;

    /// <summary>
    /// 손에서 해제할때 델리게이트
    /// </summary>
    public Action DisOnHold;

    /// <summary>
    /// 사용 버튼 누를시 활성화되는 함수 모음
    /// </summary>
    public Action UseDelegate;

    /// <summary>
    /// 사용 버튼을 땔때 활성화 되는 함수 모음
    /// </summary>
    public Action StopDelegate;

    /// <summary>
    /// 재장전
    /// </summary>
    public Action ReAction;

    /// <summary>
    /// 업데이트용 함수 델리게이트
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
    /// 플레이어가 장비했을때 장비에서 실행될 함수.
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
    /// 플레이어가 장비를 해제했을때 장비에서 실행될 함수.
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
