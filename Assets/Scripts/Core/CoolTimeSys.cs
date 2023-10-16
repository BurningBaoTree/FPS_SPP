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
    public coolClock[] coolclocks = new coolClock[5];
    Action[] Checker = new Action[5];
    Action updateCoolTime;
    public float timeCount = 0f;
    void Start()
    {
        updateCoolTime = WeWantNoNull;
        for (int i = 0; i < 10; i++)
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
        Debug.Log($"{index}��Ÿ�� ����.{time}");
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
            Debug.Log($"{index}���� ȣ�� �ð�{time} �� ��ŭ �߰�");
            coolclocks[index].time += time;
        }
        updateCoolTime += Checker[index];
    }
    void timeCheck(int index, float time)
    {
        if (timeCount > time)
        {
            Debug.Log($"{index}��Ÿ����.{timeCount}�� ����");
            coolclocks[index].time = 0f;
            coolclocks[index].coolEnd = true;
            coolclocks[index].coolStart = false;
            updateCoolTime -= Checker[index];
            Checker[index] = null;
            if (EndCheck())
            {
                updateCoolTime -= timeCounting;
                timeCount = 0f;
                Debug.Log("��� ���� ����Ǿ���.");
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
