using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Equipments equipments;

    bool UseAbleCheck = false;

    public Action IsUseAble;
    public Action IsEquitable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Equipments>() != null)
        {
            equipments = other.GetComponent<Equipments>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        equipments = null;
    }
    public void UseActiveatebyType()
    {
        if(UseAbleCheck)
        {
            IsUseAble?.Invoke();
        }
        else
        {
            IsEquitable?.Invoke();
        }
    }
}
