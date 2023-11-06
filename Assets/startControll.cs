using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startControll : MonoBehaviour
{
    public void startbutton()
    {
        SceneController.Inst.LoadingActivate(1);
    }
    public void optionbutton()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
