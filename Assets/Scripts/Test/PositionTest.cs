using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionTest : TestBase
{
    Equiped player;
    private void Start()
    {
        player = FindObjectOfType<Equiped>();
    }


    protected override void Test(InputAction.CallbackContext context)
    {
        player.equiptableList[0].transform.localPosition = Vector3.zero;
    }
    protected override void Test1(InputAction.CallbackContext context)
    {
        player.equiptableList[0].transform.Translate(Vector3.zero);
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        player.equiptableList[0].transform.Translate(Vector3.zero,Space.World);
    }
    protected override void Test3(InputAction.CallbackContext context)
    {
        player.equiptableList[0].transform.Translate(Vector3.zero, Space.Self);
    }
}
