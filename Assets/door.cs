using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class door : MonoBehaviour
{
    public Vector3 open;
    public Vector3 close;
    public float speed = 5;

    bool openup = false;

    System.Action Updater;
    private void OnEnable()
    {
        this.transform.position = close;
        GameManager.Inst.IsGameStart += boolsellect;
        Updater += DoorOpen;
    }
    void boolsellect()
    {
        openup = true;
    }
    void DoorOpen()
    {
        if (openup)
            this.transform.position = Vector3.MoveTowards(this.transform.position, open, speed * Time.deltaTime);
    }
    private void Update()
    {
        Updater();
    }
}
