using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerMove playermove;
    public PlayerMove Playermove => playermove;

    Equiped playerequiped;
    public Equiped PlayerEquiped => playerequiped;

    PlayerCam playercam;
    public PlayerCam PlayerCam => playercam;
    private void OnEnable()
    {
        playermove = FindObjectOfType<PlayerMove>();
        playerequiped = FindObjectOfType<Equiped>();
        playercam = FindObjectOfType<PlayerCam>();
    }
}

