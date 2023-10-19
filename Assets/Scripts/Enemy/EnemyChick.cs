using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChick : EnemyBase
{
    Animator animator;
    CoolTimeSys cooltime;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        cooltime = GetComponent<CoolTimeSys>();
    }

}
