using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equiped : MonoBehaviour
{
    public GameObject LeftWeaponSlot;
    public GameObject RightWeaponSlot;

    Animator animator;

    public List<Equipments> EquiptableList = new List<Equipments>(5);

    public AbilityBase ability1;
    public AbilityBase ability2;
    public AbilityBase Ult;

    public Quaternion weaponrote;

    int previous = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        HoldThis(1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            foreach(var obj in EquiptableList)
            {
                if (obj == null) 
                {
                    EquiptableList.Add(collision.gameObject.GetComponent<Equipments>());
                    obj.col.enabled = false;
                    obj.rig.isKinematic = true;
                    obj.transform.parent = RightWeaponSlot.transform;
                    break;
                }
                else
                {
                    Debug.Log("장비가 꽉 참");
                }
            }
        }
    }

    public void HoldThis(int num)
    {
        if (EquiptableList[num] != null)
        {
            EquiptableList[num].gameObject.SetActive(true);
            previous = num;
        }
        else if(num == previous)
        {
            EquiptableList[num].gameObject.SetActive(false);
            previous = 0;
        }
        else
        {
            EquiptableList[num].gameObject.SetActive(false);
            previous = 0;
            animator.SetLayerWeight(3, 0);
        }
    }
    public void DropThis(Equipments obj)
    {
        //들고있는 무기 및 장비를 버린다. 버릴때 콜라이더를 활성화 시키고 리지디 바디를 다이나믹으로(방금 막 떠오른 방법 : 최상위에 클래스를 하나 더 만들어서 모든 장비용 클래스를 만든다)
        obj.transform.parent = null;
        
    }

}
