using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
	[Header("����Ʈ ����Ʈ")] 
	[Tooltip("�Ӹ� ����Ʈ�� �̰��� ��������")]
	public Transform HeadJoint;


	PlayerInput playerinput;

	/// <summary>
	/// �÷��̾��� ��ġ�� ������ ��Ʈ��ũ����
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
