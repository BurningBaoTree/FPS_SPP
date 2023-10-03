using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    protected CoolTimeSys cooltimer;


    public Rigidbody rig;
    public BoxCollider col;

    public Action IsEquiped;
    public Action DisEquiped;
    public Action UseDelegate;
    public Action StopDelegate;

    public Action Updater;

    protected Vector3 equipPos;
    protected Quaternion equipRot;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }
    protected virtual void OnEnable()
    {
        IsEquiped += playerEquiped;
        DisEquiped += playerDisEquiped;
        cooltimer = CoolTimeSys.Inst;
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


}
