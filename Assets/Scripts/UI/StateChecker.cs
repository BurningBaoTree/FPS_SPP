using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateChecker : MonoBehaviour
{
    TextMeshProUGUI proText;
    PlayerMove pm;

    private void Awake()
    {
        proText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        pm = GameManager.Inst.Playermove;
        pm.stateChange += statechanged;
    }
    void statechanged(string data)
    {
        proText.text = data;
    }
}
