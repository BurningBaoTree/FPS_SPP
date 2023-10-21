using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUIScript : MonoBehaviour
{
    public TextMeshProUGUI scorerate;
    public TextMeshProUGUI myMessage;

    int scorenow;
    string formnum;
    string massage;

    private void OnEnable()
    {
        scorenow = GameManager.Inst.Score;
        formnum = scorenow.ToString("N0");
        scorerate.text = formnum;
        if (scorenow == 0)
        {
            massage = "������ �ϱ�� �Ѱų�?";
        }
        else if (scorenow < 5)
        {
            massage = "��... FPS ���� �� �����Ͻó�����.";
        }
        else if (scorenow < 15)
        {
            massage = "��.. ���� ��.��.�ϼž� �ڴµ�?";
        }
        else if (scorenow < 25)
        {
            massage = "��.. �����̳׿�.";
        }
        else if (scorenow < 50)
        {
            massage = "���������� �����";
        }
        else if (scorenow < 100)
        {
            massage = "5���� 1�� ���� ġŲ���� �������ȴ�.";
        }
        else if (scorenow < 150)
        {
            massage = "����~ �� �̷��� ��~";
        }
        else if (scorenow < 200)
        {
            massage = "ġŲ�� ���Ҿ��� ���� ��ʴϴ�.";
        }
        else if (scorenow < 250)
        {
            massage = "����� �׿���!! �� �ߵ鿡�Ե� ������ �ִٰ�!";
        }
        else
        {
            massage = "��... �󵵵� ���Ͱ��̽�Ʈ?";
        }
        myMessage.text = "ģ���� �޽��� : " + massage;
    }
}
