using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
	[Header("조인트 리스트")] 
	[Tooltip("머리 조인트를 이곳에 넣으세요")]
	public Transform HeadJoint;


	PlayerInput playerinput;

	/// <summary>
	/// 플레이어의 위치를 결정할 네트워크변수
	/// </summary>
	public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
	public NetworkVariable<Vector2> MoveDir = new NetworkVariable<Vector2>();


	private void Awake()
	{
		position.OnValueChanged += OnPositionChange;
	}


	public override void OnNetworkSpawn()
	{
		if (IsOwner)
		{
			transform.position = Vector3.up * 10;
			playerinput = new PlayerInput();
			playerinput.Move.Enable();
			playerinput.Move.WASD.performed += MoveAction;
			playerinput.Move.WASD.canceled += MoveAction;
		}
	}
	private void OnDisable()
	{
		if (playerinput != null)
		{
			playerinput.Move.WASD.canceled -= MoveAction;
			playerinput.Move.WASD.performed -= MoveAction;
			playerinput.Move.Disable();
		}
	}

	private void MoveAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		Vector2 move = context.ReadValue<Vector2>();
		Debug.Log($"{move}");
		if (NetworkManager.Singleton.IsServer)
		{
			MoveDir.Value = move;
		}
		else
		{
			MoveRequestServerRpc(move);
		}
	}



	[ServerRpc]
	void MoveRequestServerRpc(Vector2 move)
	{
		MoveDir.Value = move;
	}

	private void OnPositionChange(Vector3 previousValue, Vector3 newValue)
	{
		transform.position = newValue;
	}


}
