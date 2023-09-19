using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{
    TextMeshProUGUI proText;

    private void Awake()
    {
        proText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        proText.text = "<b>Hello</b> Test";
    }
}
