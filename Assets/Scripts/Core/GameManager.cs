using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerMove playermove;
    public PlayerMove Playermove => playermove;

    Equiped playerequiped;
    public Equiped PlayerEquiped => playerequiped;
    private void OnEnable()
    {
        playermove = FindObjectOfType<PlayerMove>();
        playerequiped = FindObjectOfType<Equiped>();
    }
}

