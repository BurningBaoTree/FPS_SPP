using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float WaitTime = 30;

    public float timeset = 180;

    public Action IsGameStart;
    public Action IsGameEnd;
    public bool gamecheck = false;
    public bool GameCheck
    {
        get
        {
            return gamecheck;
        }
        set
        {
            if (gamecheck != value)
            {
                gamecheck = value;
                if (gamecheck)
                {
                    IsGameStart?.Invoke();
                }
                else
                {
                    IsGameEnd?.Invoke();
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }

    public float timecode;
    public float Timecode
    {
        get
        {
            return timecode;
        }
        set
        {
            timecode = value;
            if (timecode < 0)
            {
                GameCheck = !GameCheck;
            }
        }
    }

    public Action<float> TimeSys;
    PlayerMove playermove;
    public PlayerMove Playermove => playermove;

    Equiped playerequiped;
    public Equiped PlayerEquiped => playerequiped;

    PlayerCam playercam;

    public Action<int> ValuChangeScore;
    int KilledChick = -1;
    public int Score
    {
        get
        {
            return KilledChick;
        }
        set
        {
            if (KilledChick != value)
            {
                KilledChick = value;
                ValuChangeScore?.Invoke(KilledChick);
            }
        }
    }

    public PlayerCam PlayerCam => playercam;


    private void OnEnable()
    {
        playermove = FindObjectOfType<PlayerMove>();
        playerequiped = FindObjectOfType<Equiped>();
        playercam = FindObjectOfType<PlayerCam>();
        Timecode = WaitTime;
        TimeSys += timecount;
        IsGameStart += () => { Timecode = timeset; };
        IsGameEnd += () => { SceneController.Inst.LoadingActivate(3); };
    }
    private void Start()
    {
        Score = 0;
    }
    private void Update()
    {
        TimeSys(Timecode);
    }
    void timecount(float count)
    {
        count -= Time.deltaTime;
        Timecode = count;
    }

    void ResettheGameManager()
    {
        Timecode = WaitTime;
        Score = 0;
    }
}

