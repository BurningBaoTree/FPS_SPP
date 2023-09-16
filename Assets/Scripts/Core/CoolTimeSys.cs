using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimeSys : Singleton<CoolTimeSys>
{
    Action UpdateCooltimer;

    public float timecounter = 0;

    public float cooltimer1 = 0;
    public float cooltimer2 = 0;
    public float cooltimer3 = 0;
    public float cooltimer4 = 0;
    public float cooltimer5 = 0;

    public bool coolActive1 = false;
    public bool coolActive2 = false;
    public bool coolActive3 = false;
    public bool coolActive4 = false;
    public bool coolActive5 = false;

    protected override void Awake()
    {
        base.Awake();
        UpdateCooltimer = WeWantNoNull;
    }

    private void Update()
    {
        UpdateCooltimer();
    }

    void timecouting()
    {
        timecounter += Time.deltaTime;
    }
    void coolTimerSys1()
    {
        if (cooltimer1 <= timecounter)
        {
            coolActive1 = false;
            cooltimer1 = 0;
            UpdateCooltimer -= coolTimerSys1;
        }
    }
    void coolTimerSys2()
    {
        if (cooltimer2 <= timecounter)
        {
            coolActive2 = false;
            cooltimer2 = 0;
            UpdateCooltimer -= coolTimerSys2;
        }
    }
    void coolTimerSys3()
    {
        if (cooltimer3 <= timecounter)
        {
            coolActive3 = false;
            cooltimer3 = 0;
            UpdateCooltimer -= coolTimerSys3;
        }
    }
    void coolTimerSys4()
    {
        if (cooltimer4 <= timecounter)
        {
            coolActive4 = false;
            cooltimer4 = 0;
            UpdateCooltimer -= coolTimerSys3;
        }
    }
    void coolTimerSys5()
    {
        if (cooltimer3 <= timecounter)
        {
            coolActive3 = false;
            cooltimer5 = 0;
            UpdateCooltimer -= coolTimerSys3;
        }
    }
    public void cooltimeStart(int cas, float time)
    {
        if (!coolActive1 && !coolActive2 && !coolActive3 && !coolActive4 && !coolActive5)
        {
            UpdateCooltimer += timecouting;
        }
        switch (cas)
        {
            case 1:
                coolActive1 = true;
                UpdateCooltimer += coolTimerSys1;
                cooltimer1 = timecounter + time;
                break;
            case 2:
                coolActive2 = true;
                UpdateCooltimer += coolTimerSys2;
                cooltimer2 = timecounter + time;
                break;
            case 3:
                coolActive3 = true;
                UpdateCooltimer += coolTimerSys3;
                cooltimer3 = timecounter + time;
                break;
            case 4:
                coolActive4 = true;
                UpdateCooltimer += coolTimerSys4;
                cooltimer4 = timecounter + time;
                break;
            case 5:
                coolActive4 = true;
                UpdateCooltimer += coolTimerSys5;
                cooltimer5 = timecounter + time;
                break;
            default:
                Debug.LogWarning("쿨타임 시작 실패");
                break;
        }
        if (timecounter != 0)
        {
            UpdateCooltimer += AllCheckTime;
        }
    }
    private void AllCheckTime()
    {
        if (!coolActive1 && !coolActive2 && !coolActive3 && !coolActive4 && !coolActive5)
        {
            UpdateCooltimer -= AllCheckTime;
            allcoolStop();
        }
    }
    /// <summary>
    /// 모든 쿨타임을 종료합니다.
    /// </summary>
    public void allcoolStop()
    {
        UpdateCooltimer -= timecouting;
        coolActive1 = false;
        cooltimer1 = 0f;
        coolActive2 = false;
        cooltimer2 = 0f;
        coolActive3 = false;
        cooltimer3 = 0f;
        coolActive4 = false;
        cooltimer4 = 0f;
        coolActive5 = false;
        cooltimer5 = 0f;
        timecounter = 0;
    }
}
