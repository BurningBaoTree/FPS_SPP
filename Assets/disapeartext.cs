using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class disapeartext : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        textMeshProUGUI.text = $"준비시간 : {GameManager.Inst.WaitTime}초 후..\n게임이 진행됩니다.";
        Destroy(this.gameObject, 10);
    }
}
