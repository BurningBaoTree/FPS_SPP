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
    float copytime;
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
        copytime = time;
    }
    private void OnEnable()
    {
        heatAction(sellector);
    }
    private void Update()
    {
        updateAction();
        Timecolor -= Time.deltaTime;
        changer.a = Timecolor / copytime;
    }
    void heatAction(heattedSellect sel)
    {
        if ((int)sel == 1)
        {
            changer = enemyColor;
            updateAction = () => { spriteRenderer.color = changer; };
        }
        else
        {
            changer = playerColor;
            updateAction = () => { spriteRenderer.color = changer; };
        }
    }

}
