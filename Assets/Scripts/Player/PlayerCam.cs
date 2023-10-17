using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Equipments equipments;

    public LoockWhatIs isthant;

    bool UseAbleCheck = false;

    public Action IsUseAble;
    public Action IsEquitable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EquiptAble")&& equipments == null)
        {
            equipments = other.GetComponent<Equipments>();
            isthant.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (equipments != null)
        {
            isthant.gameObject.SetActive(false);
            equipments = null;
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
