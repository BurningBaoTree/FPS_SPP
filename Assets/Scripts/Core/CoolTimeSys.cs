using System;
using System.Threading;
using UnityEngine;


[Serializable]
public struct coolClock
{
    public float time;
    public bool coolStart;
    public bool coolEnd;
}
public class CoolTimeSys : MonoBehaviour
{
    [Header("쿨타임 시계 개수")]
    public int CoolClocksCount = 5;
    public coolClock[] coolclocks;
    Action[] Checker;
    Action updateCoolTime;
    public float timeCount = 0f;
    private void Awake()
    {
        Checker = new Action[CoolClocksCount];
        coolclocks = new coolClock[CoolClocksCount];
    }
    void Start()
    {
        updateCoolTime = WeWantNoNull;
        for (int i = 0; i < CoolClocksCount; i++)
        {
            coolclocks[i].time = 0f;
            coolclocks[i].coolStart = false;
            coolclocks[i].coolEnd = true;
        }
    }

    private void Update()
    {
        updateCoolTime();
    }
    public void CoolTimeStart(int index, float time)
    {
        Checker[index] += () => { timeCheck(index, time); };
        if (startCheck())
        {
            coolclocks[index].time = time;
            updateCoolTime += timeCounting;
            coolclocks[index].coolStart = true;
            coolclocks[index].coolEnd = false;
        }
        else
        {
            coolclocks[index].time += time;
        }
        updateCoolTime += Checker[index];
    }
    void timeCheck(int index, float time)
    {
        if (timeCount > time)
        {
            coolclocks[index].time = 0f;
            coolclocks[index].coolEnd = true;
            coolclocks[index].coolStart = false;
            updateCoolTime -= Checker[index];
            Checker[index] = null;
            if (EndCheck())
            {
                updateCoolTime -= timeCounting;
                timeCount = 0f;
            }
        }
    }
    protected void WeWantNoNull()
    {

    }
    void timeCounting()
    {
        timeCount += Time.deltaTime;
    }

    bool startCheck()
    {
        foreach (coolClock c in coolclocks)
        {
            if (c.coolStart)
            {
                return false;
            }
        }
        return true;
    }
    bool EndCheck()
    {
        foreach (coolClock c in coolclocks)
        {
            if (!c.coolEnd)
            {
                return false;
            }
        }
        return true;
    }
}
