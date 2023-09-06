using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMove : MonoBehaviour
{
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

    Animator animator;

    private void Awake()
    {
		rig = GetComponent<Rigidbody>();
		playerinput = new PlayerInput();
        playerinput.Move.Enable();
        playerinput.Move.Head.performed += HeadBanging;
        playerinput.Move.WASD.performed += MoveAction;
        playerinput.Move.WASD.canceled += MoveAction;
        playerinput.Move.Jump.performed += JumpAction;
		playerinput.Move.Jump.canceled += JumpAction;
		xxis = 0;
        yxis = 0;
		animator = GetComponent<Animator>();
	}

	private void JumpAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
        rig.AddForce(Vector3.up*5, ForceMode.Impulse);
	}

	private void OnDisable()
    {
		playerinput.Move.Jump.canceled -= JumpAction;
		playerinput.Move.Jump.performed -= JumpAction;
		playerinput.Move.WASD.canceled -= MoveAction;
        playerinput.Move.WASD.performed -= MoveAction;
        playerinput.Move.Disable();
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

    private void MoveAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        MoveDir = move;
        posi.x = MoveDir.x;
        posi.z = MoveDir.y;
	}

    private void FixedUpdate()
    {
		Debug.Log($"{MoveDir.y}");
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
		this.transform.rotation = Quaternion.Euler(0, yxis, 0);
		Cameratransform.rotation = Quaternion.Euler(xxis, yxis, 0);
        HeadJoint.rotation = Quaternion.Euler(xxis, 0, 0);
        transform.Translate(speed*Time.deltaTime*posi);
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
}
