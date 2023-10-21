using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChick : EnemyBase
{
    Animator animator;
    CoolTimeSys cooltime;
    Rigidbody body;
    public float speed;
    public int jumpMax = 2;
    public float jumpstr;
    public int JumpMax
    {
        get
        {
            return jumpMax;
        }
        set
        {
            jumpMax = value;
            if (jumpMax < 1)
            {
                jumpcheck = false;
                jumpMax = Random.Range(0, jumpMax);
            }
        }
    }
    public Vector3 dir = Vector3.zero;
    public bool jumpcheck = false;

    protected override void Awake()
    {
        base.Awake();
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cooltime = GetComponent<CoolTimeSys>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            jumpcheck = true;
        }
    }
    private void OnEnable()
    {
        animator.SetBool("Run", true);
    }
    private void Update()
    {
        changeDir();
        jump();
        transform.Translate(dir * Time.deltaTime * speed, Space.World);
        transform.rotation = Quaternion.LookRotation(dir);
    }
    void changeDir()
    {
        if (cooltime.coolclocks[0].coolEnd)
        {
            dir.x = Random.Range(-1f, 1.1f);
            dir.z = Random.Range(-1f, 1.1f);
            cooltime.CoolTimeStart(0, Random.Range(1f, 10f));
        }
    }
    void jump()
    {
        if (cooltime.coolclocks[1].coolEnd && jumpcheck)
        {
            body.AddForce(Vector3.up*jumpstr, ForceMode.Impulse);
            cooltime.CoolTimeStart(1, Random.Range(2f, 5f));
            JumpMax--;
        }
    }
}
