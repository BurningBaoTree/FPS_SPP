using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    [Header("����Ʈ ����Ʈ")]
    [Tooltip("�Ӹ� ����Ʈ�� �̰��� ��������")]
    public Transform HeadJoint;
    public Transform Cameratransform;


    PlayerInput playerinput;
    public bool newState;

    Vector3 posi;
    Vector2 MoveDir;
    CharacterController charactercontroller;

    public float speed = 1.0f;
    public float headrotationSpeed = 1.0f;

    float xxis;
    float yxis;

    private void Awake()
    {
        charactercontroller = GetComponent<CharacterController>();
        playerinput = new PlayerInput();
        playerinput.Move.Enable();
        playerinput.Move.Head.performed += HeadBanging;
        playerinput.Move.WASD.performed += MoveAction;
        playerinput.Move.WASD.canceled += MoveAction;
    }



    private void OnDisable()
    {
        playerinput.Move.WASD.canceled -= MoveAction;
        playerinput.Move.WASD.performed -= MoveAction;
        playerinput.Move.Disable();
    }

    private void HeadBanging(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        //���콺�� ����� ���ϵ��� �ϴ� �ɼǿ� 3�׽��� �Ἥ  newState�� ���̸� ��װ� ���� �ƴϸ� Ǯ���ش�.
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

        Vector3 pos = context.ReadValue<Vector2>();
        float multiplyer = Time.deltaTime * headrotationSpeed;

        float xis = 0;
        float yis = 0;
        xis += multiplyer * (-pos.y);
        yis += multiplyer * (pos.x);
        xxis = ClampAngle(xis, -90, 90);
        yxis = ClampAngle(yis, float.MinValue, float.MaxValue);
        Debug.Log($"{(int)xxis},{(int)yxis},{pos}");


    }

    private void MoveAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        MoveDir = move;
        posi.x = MoveDir.x;
        posi.z = MoveDir.y;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        HeadJoint.rotation = Quaternion.Euler(xxis, yxis, 0);
        Cameratransform.rotation = Quaternion.Euler(xxis, yxis, 0);
        charactercontroller.Move(speed * Time.deltaTime * posi);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        //ifAngle���� 360�̶�� ���ڸ� ���� �ȵ��� �����ִ� ���ǹ�
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
