using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("����Ʈ ����Ʈ")] 
	[Tooltip("�Ӹ� ����Ʈ�� �̰��� ��������")]
	public Transform HeadJoint;


	PlayerInput playerinput;

	private void Awake()
	{
		
	}
	private void OnEnable()
	{
		playerinput.Enable();
	}
}
