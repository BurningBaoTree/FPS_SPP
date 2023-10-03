using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatPanel : MonoBehaviour
{
    Action updateAction;
    public enum heattedSellect
    {
        player = 0,
        enemy
    }
    public heattedSellect sellector = heattedSellect.player;

    SpriteRenderer spriteRenderer;
    public float time = 5.0f;
    float Timecolor
    {
        get
        {
            return time;
        }
        set
        {
            if (time != value)
            {
                time = value;
                if (time < 0)
                {
                    Destroy(this.gameObject);
                }

            }
        }
    }

    public Color playerColor = Color.white;
    public Color enemyColor = Color.white;
    Color changer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        heatAction(sellector, out changer);
    }
    private void Update()
    {
        updateAction();
        Timecolor -= Time.deltaTime;
        changer.a = Timecolor / time;
    }
    void heatAction(heattedSellect sel, out Color col)
    {
        if ((int)sel == 1)
        {
            updateAction = () => { spriteRenderer.color = enemyColor; };
        }
        else
        {
            updateAction = () => { spriteRenderer.color = playerColor; };
        }
        col = spriteRenderer.color;
    }

}
