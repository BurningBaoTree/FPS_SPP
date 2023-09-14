using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBase : MonoBehaviour
{
    PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Test.Enable();
        input.Test.Test.performed += Test;
        input.Test.Test1.performed += Test1;
        input.Test.Test2.performed += Test2;
        input.Test.Test3.performed += Test3;
    }
    private void OnDisable()
    {
        input.Test.Test3.performed -= Test3;
        input.Test.Test2.performed -= Test2;
        input.Test.Test1.performed -= Test1;
        input.Test.Test.performed -= Test;
        input.Test.Disable();
    }

    protected virtual void Test(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }
    protected virtual void Test1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void Test2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }
    protected virtual void Test3(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

}
