using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    public Rigidbody rig;
    public BoxCollider col;

    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }
}
