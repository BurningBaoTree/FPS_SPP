using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    public Rigidbody rig;
    public BoxCollider col;

    public Action IsEquiped;
    public Action DisEquiped;
    public Action UseDelegate;
    public Action StopDelegate;

    protected Vector3 equipPos;
    protected Quaternion equipRot;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }
    private void OnEnable()
    {
        IsEquiped += playerEquiped;
        DisEquiped += playerDisEquiped;
    }
    private void Start()
    {
        IsEquiped += playerEquiped;
        DisEquiped += playerDisEquiped;
    }
    private void OnDisable()
    {
        DisEquiped -= playerDisEquiped;
        IsEquiped -= playerEquiped;
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
