using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


[RequireComponent(typeof(Equiped))]
public class PlayerMove : MonoBehaviour
{
    Equiped eqiSys;
    [Header("조인트 리스트")]
    [Tooltip("머리 조인트를 이곳에 넣으세요")]
    public Transform HeadJoint;
    public Transform Cameratransform;

    Rigidbody rig;

    PlayerInput playerinput;
    public bool newState;

    Vector3 posi;
    Vector2 MoveDir;

    public float speed = 1.0f;
    public float headrotationSpeed = 1.0f;

    float xis = 0;
    float yis = 0;

    float xxis;
    float yxis;

    float dropgage = 0f;
    public float maxDropgage = 10f;
    float drag = 0f;


    Animator animator;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        playerinput = new PlayerInput();
        xxis = 0;
        yxis = 0;
        animator = GetComponent<Animator>();
        eqiSys = GetComponent<Equiped>();
    }

    private void OnEnable()
    {
        playerinput.Move.Enable();
        playerinput.Move.Head.performed += HeadBanging;
        playerinput.Move.WASD.performed += MoveAction;
        playerinput.Move.WASD.canceled += MoveAction;
        playerinput.Move.Jump.performed += JumpAction;

        playerinput.Move.GearChanger4.performed += WeaponSellect5;
        playerinput.Move.GearChanger3.performed += WeaponSellect4;
        playerinput.Move.GearChanger2.performed += WeaponSellect3;
        playerinput.Move.GearChanger1.performed += WeaponSellect2;
        playerinput.Move.GearChanger.performed += WeaponSellect1;

        playerinput.Move.Drop.performed += DropReady;
        playerinput.Move.Drop.canceled += DropCanceled;
    }


    private void OnDisable()
    {
        playerinput.Move.Drop.canceled -= DropCanceled;
        playerinput.Move.Drop.performed -= DropReady;

        playerinput.Move.GearChanger.performed -= WeaponSellect1;
        playerinput.Move.GearChanger1.performed -= WeaponSellect2;
        playerinput.Move.GearChanger2.performed -= WeaponSellect3;
        playerinput.Move.GearChanger3.performed -= WeaponSellect4;
        playerinput.Move.GearChanger4.performed -= WeaponSellect5;

        playerinput.Move.Jump.performed -= JumpAction;
        playerinput.Move.WASD.canceled -= MoveAction;
        playerinput.Move.WASD.performed -= MoveAction;
        playerinput.Move.Disable();
    }


    private void WeaponSellect1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.HoldThis(0);
    }
    private void WeaponSellect2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.HoldThis(1);
    }
    private void WeaponSellect3(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.HoldThis(2);
    }
    private void WeaponSellect4(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.HoldThis(3);
    }
    private void WeaponSellect5(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.HoldThis(4);
    }
    private void DropCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        drag = 0;
        dropgage = 0;
    }

    private void HeadBanging(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector3 pos = Vector3.zero;
        //마우스가 가운데를 향하도록 하는 옵션에 3항식을 써서  newState가 참이면 잠그고 참이 아니면 풀어준다.
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

        pos = context.ReadValue<Vector2>();
        float multiply = headrotationSpeed * Time.deltaTime;
        xis += multiply * (-pos.y);
        yis += multiply * (pos.x);
        if (xis > 90)
        {
            xis = 90;
        }
        if (xis < -90)
        {
            xis = -90;
        }
        xxis = ClampAngleY(xis, -90, 90);
        yxis = ClampAngle(yis, float.MinValue, float.MaxValue);
    }



    /*    private void DropActive(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            dropgage = 0;
        }*/


    private void DropReady(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        drag = context.ReadValue<float>();
    }

    private void JumpAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        rig.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }
    private void MoveAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        MoveDir = move;
        posi.x = MoveDir.x;
        posi.z = MoveDir.y;
        if (MoveDir.y > 0.5f)
        {
            animator.SetInteger("walk", 1);
        }
        else if (MoveDir.y < -0.5f)
        {
            animator.SetInteger("walk", -1);
        }
        else
        {
            animator.SetInteger("walk", 0);
        }
    }

    private void FixedUpdate()
    {
        dropgage += drag/3f;
        if (dropgage > maxDropgage)
        {
            eqiSys.DropThis();
            dropgage = 0;
        }
        this.transform.rotation = Quaternion.Euler(0, yxis, 0);
        Cameratransform.rotation = Quaternion.Euler(xxis, yxis, 0);
        HeadJoint.rotation = Quaternion.Euler(xxis, 0, 0);
        transform.Translate(speed * Time.fixedDeltaTime * posi);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        //ifAngle값이 360이라는 숫자를 넘지 안도록 막아주는 조건문
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    private static float ClampAngleY(float lfAngle, float lfMin, float lfMax)
    {
        //ifAngle값이 360이라는 숫자를 넘지 안도록 막아주는 조건문
        if (lfAngle < lfMin) lfAngle = lfMin;
        if (lfAngle > lfMax) lfAngle = lfMax;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    void DropThings()
    {

    }
}
