using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Equiped : MonoBehaviour
{
    /// <summary>
    /// ��Ÿ�̸�
    /// </summary>
    CoolTimeSys coolsys;

    /// <summary>
    /// ���� ī�޶�
    /// </summary>
    public PlayerCam plcam;


    /// <summary>
    /// ���ʼ� ���� ����
    /// </summary>
    public GameObject LeftWeaponSlot;
    /// <summary>
    /// ������ ���� ����
    /// </summary>
    public GameObject RightWeaponSlot;

    /// <summary>
    /// �ִϸ�����
    /// </summary>
    Animator animator;

    /// <summary>
    /// ��� ����Ʈ(�κ��丮)
    /// </summary>
    public Equipments[] equiptableList;

    /// <summary>
    /// �ɷ�1
    /// </summary>
    public AbilityBase ability1;
    /// <summary>
    /// �ɷ�2
    /// </summary>
    public AbilityBase ability2;
    /// <summary>
    /// �ñر�
    /// </summary>
    public AbilityBase Ult;

    /// <summary>
    /// ���� ��ġ(IK��)
    /// </summary>
    public Vector3 weaponPos;

    public Action SlotChanged;

    /// <summary>
    /// �� �񱳿� int
    /// </summary>
    public int previous;
    public int Previous
    {
        get
        {
            return previous;
        }
        set
        {
            if (previous != value)
            {
                previous = value;
                SlotChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// IK�� �������� �Ѿư� ������Ʈ
    /// </summary>
    public Transform rightTartge;
    /// <summary>
    /// IK�� ���ʼ��� �Ѿư� ������Ʈ
    /// </summary>
    public Transform leftTartge;

    /// <summary>
    /// IK�� ���̾� ����
    /// </summary>
    int layerState = 0;

    /// <summary>
    /// �ִϸ��̼� ���̾� ������ float
    /// </summary>
    float animatorLayerWeightWeapon = 0;

    /// <summary>
    /// ��ü �ð�
    /// </summary>
    public float changingTime = 0f;

    /// <summary>
    /// ������Ʈ ����� ��������Ʈ
    /// </summary>
    Action animatorLayerWeight;




    private void Awake()
    {
        animatorLayerWeight = nullablerVoid;
        animator = GetComponent<Animator>();
        equiptableList = new Equipments[6];
    }
    private void OnEnable()
    {
        plcam.IsEquitable += AddGeartoInven;
    }
    private void Start()
    {
        animator.SetLayerWeight(1, 0);
        coolsys = transform.GetComponent<CoolTimeSys>();
    }
    private void OnDisable()
    {
        plcam.IsEquitable -= AddGeartoInven;
    }
    private void Update()
    {
        animatorLayerWeight();
    }

    /// <summary>
    /// IK������ �����
    /// </summary>
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

    /// <summary>
    /// EŰ�� �������� ����Ǵ� �Լ��̴�.(��뿡 ���� �ٷ� ����ϰų� ���� �� ����Ѵ�.)
    /// </summary>
    public void useThis()
    {
        plcam.UseActiveatebyType();
    }

    /// <summary>
    /// �����ִ� ��� �κ��丮�� �ű�� �Լ�
    /// </summary>
    public void AddGeartoInven()
    {
        if (plcam.Equipments != null && coolsys.coolclocks[1].coolEnd)
        {
            for (int i = 0; i < 5;)
            {
                if (equiptableList[i] == null)
                {
                    equiptableList[i] = plcam.Equipments;
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
                    Debug.Log("���̻� ��� �߰� �Ұ�");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// ���� �տ� ���� ��� ��� �ϴ� �Լ�.
    /// </summary>
    /// <param name="num">�κ��丮 ��(�ּ�)</param>
    public void HoldThis(int num)
    {
        if (equiptableList[num] != null && equiptableList[Previous] == null)
        {
            equiptableList[num].gameObject.SetActive(true);
            equiptableList[num].IsOnHold?.Invoke();
            Previous = num;
            changingTime = 1;
            animatorLayerWeightWeapon = 0;
            animatorLayerWeight += AnimatorLayerSetter1;
            layerState = 1;
        }
        else if (equiptableList[num] != null && num != Previous)
        {
            equiptableList[num].gameObject.SetActive(true);
            equiptableList[num].IsOnHold?.Invoke();
            equiptableList[Previous].DisOnHold?.Invoke();
            equiptableList[Previous].gameObject.SetActive(false);
            equiptableList[Previous].bulletReduced = null;
            Previous = num;
            changingTime = 1;
            animatorLayerWeightWeapon = 0;
            animatorLayerWeight += AnimatorLayerSetter1;
            layerState = 1;
        }
        else if (equiptableList[num] == null && equiptableList[Previous] != null)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
            animatorLayerWeightWeapon = 0;
            equiptableList[Previous].DisOnHold?.Invoke();
            equiptableList[Previous].gameObject.SetActive(false);
            equiptableList[Previous].bulletReduced = null;
            Previous = 5;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
        else if (equiptableList[num] == null)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
            animatorLayerWeightWeapon = 0;
            Previous = 5;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
        else if (num == Previous)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
            animatorLayerWeightWeapon = 0;
            equiptableList[num].DisOnHold?.Invoke();
           equiptableList[num].gameObject.SetActive(false);
            equiptableList[num].bulletReduced = null;
            Previous = 5;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
        }
    }

    /// <summary>
    /// ���콺 ��Ŭ���� ���� �տ� ����ִ� ��� �����Ѵ�.
    /// </summary>
    public void ActionThis()
    {
        if (equiptableList[Previous] != null)
        {
            equiptableList[Previous].UseDelegate?.Invoke();
        }
    }

    /// <summary>
    /// ���콺 ��Ŭ���� ������ ���� �տ� ����ִ� ��� �����Ѵ�.
    /// </summary>
    public void stopActionThis()
    {
        if (equiptableList[Previous] != null)
        {
            equiptableList[Previous].StopDelegate?.Invoke();
        }
    }

    /// <summary>
    /// RŰ�� ������ ����Ǵ� �Լ�.
    /// </summary>
    public void ReActionThis()
    {
        if (equiptableList[Previous] != null)
        {
            equiptableList[Previous].ReAction?.Invoke();
        }
    }

    /// <summary>
    /// �ִϸ��̼� ���� �Լ�
    /// </summary>
    void AnimatorLayerSetter1()
    {
        animatorLayerWeightWeapon += Time.deltaTime / changingTime;
        animator.SetLayerWeight(1, animatorLayerWeightWeapon);
        if (animatorLayerWeightWeapon >= 1)
        {
            animatorLayerWeight -= AnimatorLayerSetter1;
        }
    }

    /// <summary>
    /// ���� �տ� ����ִ� ��� ����߸��� �Լ�
    /// </summary>
    public void DropThis()
    {
        if (equiptableList[Previous] != null)
        {
            coolsys.CoolTimeStart(1, 0.5f);
            equiptableList[Previous].transform.parent = null;
            equiptableList[Previous].DisEquiped.Invoke();
            equiptableList[Previous].bulletReduced = null;
            equiptableList[Previous] = null;
            animator.SetLayerWeight(1, 0);
            layerState = 0;
            Previous = 5;
        }
    }

    /// <summary>
    /// �� ����
    /// </summary>
    void nullablerVoid()
    {

    }
}