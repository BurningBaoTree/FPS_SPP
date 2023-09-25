using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Slider : MonoBehaviour
{
    public PlayerMove ply;
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
    }
}
