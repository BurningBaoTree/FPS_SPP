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
            massage = "게임을 하기는 한거냐?";
        }
        else if (scorenow < 5)
        {
            massage = "음... FPS 별로 안 좋아하시나봐요.";
        }
        else if (scorenow < 15)
        {
            massage = "음.. 좀더 분.발.하셔야 겠는데?";
        }
        else if (scorenow < 25)
        {
            massage = "흠.. 허접이네요.";
        }
        else if (scorenow < 50)
        {
            massage = "부족하지만 낫배드";
        }
        else if (scorenow < 100)
        {
            massage = "5분의 1의 닭을 치킨으로 만들어버렸다.";
        }
        else if (scorenow < 150)
        {
            massage = "어휴~ 뭘 이런걸 다~";
        }
        else if (scorenow < 200)
        {
            massage = "치킨집 아죠씨가 웃고 계십니다.";
        }
        else if (scorenow < 250)
        {
            massage = "당신이 죽였어!! 그 닭들에게도 가족이 있다고!";
        }
        else
        {
            massage = "와... 상도동 폴터가이스트?";
        }
        myMessage.text = "친구의 메시지 : " + massage;
    }
}
