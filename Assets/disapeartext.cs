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
        textMeshProUGUI.text = $"�غ�ð� : {GameManager.Inst.WaitTime}�� ��..\n������ ����˴ϴ�.";
        Destroy(this.gameObject, 10);
    }
}
