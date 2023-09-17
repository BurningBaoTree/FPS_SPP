using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Equiped : MonoBehaviour
{
    CoolTimeSys coolsys;

    public GameObject LeftWeaponSlot;
    public GameObject RightWeaponSlot;

    Animator animator;

    public Equipments[] equiptableList;

    public AbilityBase ability1;
    public AbilityBase ability2;
    public AbilityBase Ult;

    public Vector3 weaponPos;

    public int previous;

    public Transform rightTartge;
    public Transform leftTartge;

    int layerState = 0;
    float animatorLayerWeightWeapon = 0;
    public float changingTime = 0f;
    Action animatorLayerWeight;

    private void Awake()
    {
        animatorLayerWeight = nullablerVoid;
        animator = GetComponent<Animator>();
        equiptableList = new Equipments[6];
    }
    private void OnEnable()
    {
        coolsys = CoolTimeSys.Inst;
    }
    private void Start()
    {
        animator.SetLayerWeight(1, 0);
    }
    private void Update()
    {
        animatorLayerWeight();
    }

    private void OnAnimatorIK()
    {
        if (layerState == 1)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, animatorLayerWeightWeapon);
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightTartge.position);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, animatorLayerWeightWeapon);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightTartge.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, animatorLayerWeightWeapon);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftTartge.position);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, animatorLayerWeightWeapon);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftTartge.rotation);
        }
        else
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Weapon") && !coolsys.coolActive1)
        {
            for (int i = 0; i < 5;)
            {
                if (equiptableList[i] == null)
                {
                    equiptableList[i] = collision.gameObject.GetComponent<Equipments>();
                    equiptableList[i].transform.parent = RightWeaponSlot.transform;
                    equiptableList[i].IsEquiped.Invoke();
                    HoldThis(i);
                    break;
                }
                else if (equiptableList[i] != null)
                {
                    i++;
                }
                else
                {
                    Debug.Log("더이상 장비 추가 불가");
                    break;
                }
            }
        }
    }

    public void HoldThis(int num)
    {

        if (equiptableList[num] != null && equiptableList[previous] == null)
        {
            equiptableList[num].gameObject.SetActive(true);
            previous = num;
            changingTime = 1;
            animatorLayerWeightWeapon = 0;
            animatorLayerWeight += AnimatorLayerSetter1;
            layerState = 1;
        }
        else if (equiptableList[num] != null && num != previous)
        {
            equiptableList[num].gameObject.SetActive(true);
            equiptableList[previous].gameObject.SetActive(false);
            previous = num;
            changingTime = 1;
            animatorLayerWeightWeapon = 0;
            animatorLayerWeight += AnimatorLayerSetter1;
            layerState = 1;
        }
        else if (equiptableList[num] == null && equiptableList[previous]!=null)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
            animatorLayerWeightWeapon = 0;
            equiptableList[previous].gameObject.SetActive(false);
            previous = 5;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
        else if (equiptableList[num] == null)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
            animatorLayerWeightWeapon = 0;
            previous = 5;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
        else if (num == previous)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
            animatorLayerWeightWeapon = 0;
            equiptableList[num].gameObject.SetActive(false);
            previous = 5;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
    }

    void AnimatorLayerSetter1()
    {
        animatorLayerWeightWeapon += Time.deltaTime / changingTime;
        animator.SetLayerWeight(1, animatorLayerWeightWeapon);
        if (animatorLayerWeightWeapon >= 1)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
        }
    }
    public void DropThis()
    {
        if (equiptableList[previous] != null)
        {
            coolsys.cooltimeStart(1, 0.5f);
            equiptableList[previous].transform.parent = null;
            equiptableList[previous].DisEquiped.Invoke();
            equiptableList[previous] = null;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
    }
    void nullablerVoid()
    {

    }
}
