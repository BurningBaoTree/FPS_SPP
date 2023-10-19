using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    GameObject DeadEfeect;
    public float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (hp != value)
            {
            hp = value;
                if (hp < 0)
                {
                    Die();
                }
            }
        }
    }
    protected virtual void Awake()
    {
        DeadEfeect = transform.GetChild(0).gameObject;
    }

    void Die()
    {
        DeadEfeect.transform.parent = null;
        DeadEfeect.SetActive(true);
        Destroy(this);
    }
}
