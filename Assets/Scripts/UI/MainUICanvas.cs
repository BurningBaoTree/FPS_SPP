using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUICanvas : MonoBehaviour
{
    Equiped eqi;
    PlayerMove plmv;
    PlayerCam cam;
    TextMeshProUGUI playerState;
    Slider bulletreamin;
    GameObject dot;
    TextMeshProUGUI bulletRemainText;
    TextMeshProUGUI gunName;
    Image reLodingSprite;
    GameObject ChrossHaire;
    TextMeshProUGUI Timeleft;
    TextMeshProUGUI Score;
    void Awake()
    {
        playerState = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        bulletreamin = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        bulletRemainText = bulletreamin.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        gunName = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        reLodingSprite = transform.GetChild(4).GetComponent<Image>();
        ChrossHaire = transform.GetChild(5).gameObject;
        Timeleft = transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        Score = transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        dot = transform.GetChild(2).gameObject;

        bulletreamin.gameObject.SetActive(false);
        ChrossHaire.SetActive(false);
        reLodingSprite.gameObject.SetActive(false);
        gunName.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        eqi = GameManager.Inst.PlayerEquiped;
        plmv = GameManager.Inst.Playermove;
        cam = GameManager.Inst.PlayerCam;
        plmv.stateChange += StateChanger;
        eqi.SlotChanged += CompairEquiptslot;
        cam.IsawSomething += WhatisThatThing;
    }

    /// <summary>
    /// 플레이어 상태에 따라 오른쪽 아래에 알려주는 함수
    /// </summary>
    /// <param name="ste"></param>
    void StateChanger(string ste)
    {
        playerState.text = ste;
    }

    /// <summary>
    /// 현재 내 손에 무기가 쥐어져 있을때 실행되는 함수
    /// </summary>
    void CompairEquiptslot()
    {
        if (eqi.equiptableList[eqi.Previous] != null)
        {
            bulletreamin.gameObject.SetActive(true);
            ChrossHaire.SetActive(true);
            dot.SetActive(false);
            InitializedSlide();
        }
        else
        {
            bulletreamin.gameObject.SetActive(false);
            ChrossHaire.SetActive(false);
            dot.SetActive(true);
        }
    }

    void WhatisThatThing()
    {
        if (cam.Equipments != null)
        {
            gunName.gameObject.SetActive(true);
            gunName.text = cam.Equipments.EquipterableName;
        }
        else
        {
            gunName.gameObject.SetActive(false);
        }
    }
    void InitializedSlide()
    {
        Debug.Log("호출");
        bulletreamin.maxValue = eqi.equiptableList[eqi.Previous].maxbullet;
        bulletreamin.value = eqi.equiptableList[eqi.Previous].Bullet;
        bulletRemainText.text = $"{bulletreamin.value:000}/{bulletreamin.maxValue:000}";
        eqi.equiptableList[eqi.Previous].bulletReduced += updateBullets;
    }
    void updateBullets()
    {
        bulletreamin.value = eqi.equiptableList[eqi.Previous].Bullet;
        bulletRemainText.text = $"{bulletreamin.value:000}/{bulletreamin.maxValue:000}";
    }
}
