using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoockWhatIs : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    PlayerCam cam;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        cam = GameManager.Inst.PlayerCam;
    }
    private void OnEnable()
    {
        textMeshProUGUI.text = cam.Equipments.EquipterableName;
    }
}
