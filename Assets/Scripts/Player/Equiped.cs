using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Equiped : MonoBehaviour
{
    /// <summary>
    /// 쿨타이머
    /// </summary>
    CoolTimeSys coolsys;

    /// <summary>
    /// 시점 카메라
    /// </summary>
    public PlayerCam plcam;


    /// <summary>
    /// 왼쪽손 무기 슬롯
    /// </summary>
    public GameObject LeftWeaponSlot;
    /// <summary>
    /// 오른손 무기 슬롯
    /// </summary>
    public GameObject RightWeaponSlot;

    /// <summary>
    /// 애니메이터
    /// </summary>
    Animator animator;

    /// <summary>
    /// 장비 리스트(인벤토리)
    /// </summary>
    public Equipments[] equiptableList;

    /// <summary>
    /// 능력1
    /// </summary>
    public AbilityBase ability1;
    /// <summary>
    /// 능력2
    /// </summary>
    public AbilityBase ability2;
    /// <summary>
    /// 궁극기
    /// </summary>
    public AbilityBase Ult;

    /// <summary>
    /// 무기 위치(IK용)
    /// </summary>
    public Vector3 weaponPos;

    public Action SlotChanged;

    /// <summary>
    /// 값 비교용 int
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
    /// IK용 오른손이 쫓아갈 오브젝트
    /// </summary>
    public Transform rightTartge;
    /// <summary>
    /// IK용 왼쪽손이 쫓아갈 오브젝트
    /// </summary>
    public Transform leftTartge;

    /// <summary>
    /// IK용 레이어 상태
    /// </summary>
    int layerState = 0;

    /// <summary>
    /// 애니메이션 레이어 설정용 float
    /// </summary>
    float animatorLayerWeightWeapon = 0;

    /// <summary>
    /// 교체 시간
    /// </summary>
    public float changingTime = 0f;

    /// <summary>
    /// 업데이트 실행용 델리게이트
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
    /// IK움직임 제어용
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
    /// E키를 눌렀을때 실행되는 함수이다.(사용에 따라 바로 사용하거나 차지 후 사용한다.)
    /// </summary>
    public void useThis()
    {
        plcam.UseActiveatebyType();
    }

    /// <summary>
    /// 보고있는 장비를 인벤토리로 옮기는 함수
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
                    Debug.Log("더이상 장비 추가 불가");
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 지금 손에 현재 장비를 들게 하는 함수.
    /// </summary>
    /// <param name="num">인벤토리 값(주소)</param>
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
    /// 마우스 좌클릭시 지금 손에 들고있는 장비를 실행한다.
    /// </summary>
    public void ActionThis()
    {
        if (equiptableList[Previous] != null)
        {
            equiptableList[Previous].UseDelegate?.Invoke();
        }
    }

    /// <summary>
    /// 마우스 좌클릭이 때지면 지금 손에 들고있는 장비를 중지한다.
    /// </summary>
    public void stopActionThis()
    {
        if (equiptableList[Previous] != null)
        {
            equiptableList[Previous].StopDelegate?.Invoke();
        }
    }

    /// <summary>
    /// R키를 누를시 실행되는 함수.
    /// </summary>
    public void ReActionThis()
    {
        if (equiptableList[Previous] != null)
        {
            equiptableList[Previous].ReAction?.Invoke();
        }
    }

    /// <summary>
    /// 애니메이션 제어 함수
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
    /// 지금 손에 들고있는 장비를 떨어뜨리는 함수
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
    /// 널 방지
    /// </summary>
    void nullablerVoid()
    {

    }
}