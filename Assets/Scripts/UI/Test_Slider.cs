using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Slider : MonoBehaviour
{
    public PlayerMove ply;
    public PlayerState state;
    public Equiped eqSys;
    public Slider[] slider;

    private void Awake()
    {
        slider = new Slider[2];
        slider[0] = transform.GetChild(0).GetComponent<Slider>();
        slider[1] = transform.GetChild(1).GetComponent<Slider>();
    }
    private void Update()
    {
        slider[0].value = ply.dropgage;
        if (eqSys.equiptableList[eqSys.Previous] != null)
        {
            if (eqSys.equiptableList[eqSys.Previous].type == Equipments.equipts.weapons)
            {
                Debug.Log("µø¿€¡ﬂ");
                slider[1].gameObject.SetActive(true);
                WeaponBAse wep = eqSys.equiptableList[eqSys.Previous].GetComponent<WeaponBAse>();
                slider[1].maxValue = wep.maxbullet;
                slider[1].value = wep.Bullet;
            }
        }
        else
        {
            slider[1].gameObject.SetActive(false);
        }
    }
}
