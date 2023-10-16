using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyTypeSellect : MonoBehaviour
{
    public enum Type
    {
        Asullt = 0,
        Luncher,
        Banguard,
        Tank,
        sniper
    }
    public Type enemyType = Type.Asullt;
    public Type EnemyType
    {
        get
        {
            return enemyType;
        }
        set
        {
            switch (enemyType)
            {
                case Type.Asullt:
                    break;
                case Type.Luncher:
                    break;
                case Type.Banguard:
                    break;
                case Type.Tank:
                    break;
                case Type.sniper:
                    break;
                default:
                    break;
            }
        }
    }
    private void Awake()
    {

    }

}
