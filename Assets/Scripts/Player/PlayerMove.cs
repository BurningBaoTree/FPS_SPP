using System;
using UnityEngine;


[RequireComponent(typeof(Equiped))]
public class PlayerMove : MonoBehaviour
{
    public enum State
    {
        Idel = 0,
        walk,
        run,
        jump
    }

    public State playerstate = 0;
    public State Playerstate
    {
        get
        {
            return playerstate;
        }
        set
        {
            switch (playerstate)
            {
                case State.Idel:
                    break;
                case State.walk:
                    break;
                case State.run:
                    break;
                case State.jump:
                    break;
                default:
                    break;
            }
            playerstate = value;
            switch (playerstate)
            {
                case State.Idel:
                    break;
                case State.walk:
                    break;
                case State.run:
                    break;
                case State.jump:
                    break;
                default:
                    playerstate = 0;
                    break;
            }
        }
    }

    Equiped eqiSys;
    public Transform Cameratransform;
    public Transform CamUpdownTransform;
    public Transform IKHand;
    public Transform dot;

    public CoolTimeSys coolsys;

    Rigidbody rig;

    PlayerInput playerinput;

    Vector3 posi;
    Vector2 MoveDir;
    Vector3 defoltTransform;
    public bool newState;



    bool updowncheck = false;
    /// <summary>
    /// 걸을때 시야가 위아래로 왔다갔다하는 프로퍼티
    /// </summary>
    bool UpdownCheck
    {
        get
        {
            return updowncheck;
        }
        set
        {
            if (updowncheck != value)
            {
                updowncheck = value;
                if (updowncheck)
                {
                    checker += updowncam;
                }
                else
                {
                    checker -= updowncam;
                    slowlycomback(CamUpdownTransform.localPosition, defoltTransform, 10);
                }
            }
        }
    }

    bool walkActive = false;
    /// <summary>
    /// 걷기 프로퍼티
    /// </summary>
    bool WalkActive
    {
        get
        {
            return walkActive;
        }
        set
        {
            if (walkActive != value)
            {
                walkActive = value;
                if (walkActive)
                {
                    UpdownCheck = true;
                    checker += WalkAction;
                    Playerstate = State.walk;
                }
                else
                {
                    UpdownCheck = false;
                    checker -= WalkAction;
                    Playerstate = State.Idel;
                }
            }
        }
    }

    bool inAir = false;
    bool jumpcheck = false;
    /// <summary>
    /// 점프 프로퍼티
    /// </summary>
    bool JumpCheck
    {
        get
        {
            return jumpcheck;
        }
        set
        {
            if (jumpcheck != value)
            {
                jumpcheck = value;
                if (jumpcheck)
                {
                    if (!inAir)
                    {
                        rig.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    }
                    Playerstate = State.jump;
                    UpdownCheck = false;
                }
                else if (WalkActive)
                {
                    Playerstate = State.walk;
                    UpdownCheck = true;
                }
                else
                {
                    Playerstate = State.Idel;
                    UpdownCheck = false;
                }
            }
        }
    }
    bool runcheck = false;
    bool RunCheck
    {
        get
        {
            return runcheck;
        }
        set
        {
            if (runcheck != value)
            {
                runcheck = value;
                if (runcheck)
                {
                    Playerstate = State.run;
                }
                else
                {
                    if (JumpCheck)
                    {
                        Playerstate = State.jump;
                    }
                    else if (WalkActive)
                    {
                        Playerstate = State.walk;
                    }
                    else
                    {
                        Playerstate = State.Idel;
                    }
                }
            }
        }
    }
    public float speed = 1.0f;
    float updonwspeed = 0f;
    float updonwAdd = 10f;
    float updonwImpulse = 0.01f;
    public float headrotationSpeed = 1.0f;

    public float xis = 0;
    float yis = 0;

    float xxis;
    float yxis;
    int ani;

    public float dropgage = 0f;
    public float maxDropgage = 10f;
    float drag = 0f;



    Animator animator;

    Action checker;
    Action useAction;

    private void Awake()
    {
        defoltTransform = CamUpdownTransform.localPosition;
        rig = GetComponent<Rigidbody>();
        playerinput = new PlayerInput();
        xxis = 0;
        yxis = 0;
        animator = GetComponent<Animator>();
        eqiSys = GetComponent<Equiped>();
        checker = WWN;
    }
    #region OnEnable & OnDisable
    private void OnEnable()
    {
        coolsys = new CoolTimeSys();
        playerinput.Move.Enable();
        playerinput.Move.Fire.performed += UseHolding;
        playerinput.Move.Fire.canceled += UNUseHolding;
        playerinput.Move.Head.performed += HeadBanging;
        playerinput.Move.WASD.performed += MoveAction;
        playerinput.Move.WASD.canceled += MoveAction;
        playerinput.Move.Dash.performed += RunActive;
        playerinput.Move.Dash.canceled += RunDeActive;
        playerinput.Move.Jump.performed += JumpAction;
        playerinput.Move.Use.performed += UseAction;
        playerinput.Move.Use.canceled += UseCanceled;

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

        playerinput.Move.Use.canceled -= UseCanceled;
        playerinput.Move.Use.performed -= UseAction;
        playerinput.Move.Jump.performed -= JumpAction;
        playerinput.Move.Dash.canceled -= RunDeActive;
        playerinput.Move.Dash.performed -= RunActive;
        playerinput.Move.WASD.canceled -= MoveAction;
        playerinput.Move.WASD.performed -= MoveAction;
        playerinput.Move.Head.performed -= HeadBanging;
        playerinput.Move.Fire.canceled -= UNUseHolding;
        playerinput.Move.Fire.performed -= UseHolding;
        playerinput.Move.Disable();
    }
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            inAir = false;
            JumpCheck = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            inAir = true;
        }
    }
    private void FixedUpdate()
    {
        //초과회전 방지
        xxis = ClampAngleY(xis, -90, 90);
        yxis = ClampAngle(yis, float.MinValue, float.MaxValue);

        //Y를 축으로 돌아가는 몸통
        this.transform.rotation = Quaternion.Euler(0, yxis, 0);
        //시야 회전
        Cameratransform.rotation = Quaternion.Euler(xxis, yxis, 0);
        //손과 장비 회전
        IKHand.localRotation = Quaternion.LookRotation(dot.localPosition);
        //업데이트 델리게이트
        checker();
    }
    #region 장비 관련(줍고 손에 들기)
    private void UseHolding(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.ActionThis();
    }
    private void UNUseHolding(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        eqiSys.stopActionThis();
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
    #endregion
    #region 상호작용
    private void DropReady(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        useAction = eqiSys.DropThis;
        checker += gageCheck;
        drag = context.ReadValue<float>();
    }
    private void DropCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        checker -= gageCheck;
        useAction = null;
        drag = 0;
        dropgage = 0;
    }
    private void UseAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        useAction = eqiSys.useThis;
        checker += gageCheck;
        drag = context.ReadValue<float>();
    }
    private void UseCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        checker -= gageCheck;
        useAction = null;
        drag = 0;
        dropgage = 0;
    }
    #endregion
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
    }
    private void JumpAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        JumpCheck = true;
    }
    private void MoveAction(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        WalkActive = true;
        Vector2 move = context.ReadValue<Vector2>();
        MoveDir = move;
        ani = (int)MoveDir.y;
        posi.x = MoveDir.x;
        posi.z = MoveDir.y;
        animator.SetInteger("walk", ani);
        if (MoveDir.sqrMagnitude < 0.8f)
        {
            WalkActive = false;
        }
    }
    void WalkAction()
    {
        //이동 
        transform.Translate(speed * Time.fixedDeltaTime * posi);
    }
    void slowlycomback(Vector3 pos, Vector3 headto, float speed)
    {
        Vector3 distance;
        void comback()
        {
            distance = pos - headto;
            CamUpdownTransform.localPosition = Vector3.MoveTowards(pos,headto,speed*Time.fixedDeltaTime);
            if(distance.sqrMagnitude < 0.1f)
            {
                checker -= comback;
            }
        }
        checker += comback;
    }
    private void RunActive(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        RunCheck = true;
        speed *= 2;
        ani = (int)(ani * 2.1f);
        updonwAdd *= 2;
        updonwImpulse *= 4;
    }
    private void RunDeActive(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        RunCheck = false;
        speed *= 0.5f;
        ani = (int)(ani * 0.5f);
        updonwAdd *= 0.5f;
        updonwImpulse *= 0.25f;

    }
    void updowncam()
    {
        updonwspeed = Mathf.Cos(Time.time * updonwAdd);
        CamUpdownTransform.Translate(Vector3.up * updonwspeed * updonwImpulse, Space.Self);
    }
    void gageCheck()
    {
        dropgage += drag * 0.1f;
        if (dropgage > maxDropgage)
        {
            useAction?.Invoke();
            dropgage = 0;
            useAction = null;
        }
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
    void WWN()
    {

    }
}
