using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletSlide : MonoBehaviour
{
    Equiped eqi;
    Slider bulletreamin;
    TextMeshProUGUI bulletRemainText;

    int firstCheck = 0;
    private void Awake()
    {
        bulletreamin = GetComponent<Slider>();
        bulletRemainText = bulletreamin.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        Equiped eqi = GameManager.Inst.PlayerEquiped;
        eqi.SlotChanged += InitializedSlide;
    }

    /// <summary>
    /// ���� ���� ��� �ִ� ���� ����
    /// </summary>
    void InitializedSlide()
    {
        if (eqi.equiptableList[eqi.Previous] != null)
        {
            Debug.Log("ȣ��");
            bulletreamin.maxValue = eqi.equiptableList[eqi.Previous].maxbullet;
            bulletreamin.value = eqi.equiptableList[eqi.Previous].Bullet;
            bulletRemainText.text = $"{bulletreamin.value:000}/{bulletreamin.maxValue:000}";
            eqi.equiptableList[eqi.Previous].bulletReduced += updateBullets;
        }
    }
    void updateBullets()
    {
        bulletreamin.value = eqi.equiptableList[eqi.Previous].Bullet;
        bulletRemainText.text = $"{bulletreamin.value:000}/{bulletreamin.maxValue:000}";
    }
}
