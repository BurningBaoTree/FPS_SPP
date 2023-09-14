using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equiped : MonoBehaviour
{
    public GameObject LeftWeaponSlot;
    public GameObject RightWeaponSlot;

    Animator animator;

    public Equipments[] equiptableList;

    public AbilityBase ability1;
    public AbilityBase ability2;
    public AbilityBase Ult;

    public Vector3 weaponPos;

    public int previous;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        equiptableList = new Equipments[6];
    }
    private void Start()
    {
        animator.SetLayerWeight(2, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            for (int i = 0; i < 5;)
            {
                if (equiptableList[i] == null)
                {
                    equiptableList[i] = collision.gameObject.GetComponent<Equipments>();
                    equiptableList[i].transform.parent = RightWeaponSlot.transform;
                    equiptableList[i].IsEquiped.Invoke();
                    HoldThis(i);
                    break;
                }
                else if (equiptableList[i] != null)
                {
                    i++;
                }
                else
                {
                    Debug.Log("더이상 장비 추가 불가");
                    break;
                }
            }
        }
    }

    public void HoldThis(int num)
    {
        if(equiptableList[num] != null && equiptableList[previous] == null)
        {
            equiptableList[num].gameObject.SetActive(true);
            previous = num;
            animator.SetLayerWeight(2, 1);
        }
        else if (equiptableList[num] != null && num != previous)
        {
            equiptableList[num].gameObject.SetActive(true);
            equiptableList[previous].gameObject.SetActive(false);
            previous = num;
            animator.SetLayerWeight(2, 1);
        }
        else if (equiptableList[num] == null)
        {
            previous = 0;
            animator.SetLayerWeight(2, 0);
        }
        else if (num == previous)
        {
            equiptableList[num].gameObject.SetActive(false);
            previous = 5;
            animator.SetLayerWeight(2, 0);
        }
        else
        {

        }
    }
    public void DropThis()
    {
        if(equiptableList[previous] != null)
        {
            equiptableList[previous].transform.parent = null;
            equiptableList[previous].DisEquiped.Invoke();
            equiptableList[previous] = null;
            animator.SetLayerWeight(2, 0);
        }
    }

}
