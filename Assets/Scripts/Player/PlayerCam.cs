using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Equipments equipments;

    public Action IsawSomething;
    public Equipments Equipments
    {
        get
        {
            return equipments;
        }
        set
        {
            equipments = value;
            IsawSomething?.Invoke();
        }
    }

    bool UseAbleCheck = false;

    public Action IsUseAble;
    public Action IsEquitable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EquiptAble") && Equipments == null)
        {
            Equipments = other.GetComponent<Equipments>();
            UseAbleCheck = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Equipments != null)
        {
            Equipments = null;
        }
    }
    public void UseActiveatebyType()
    {
        if (UseAbleCheck)
        {
            IsUseAble?.Invoke();
        }
        else
        {
            IsEquitable?.Invoke();
        }
    }
}
