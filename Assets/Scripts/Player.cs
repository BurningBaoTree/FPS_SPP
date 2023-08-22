using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("조인트 리스트")] 
	[Tooltip("머리 조인트를 이곳에 넣으세요")]
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
