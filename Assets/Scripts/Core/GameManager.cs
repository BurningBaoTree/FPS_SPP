using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float WaitTime = 30;

    public float timeset = 180;

    public Action IsGameStart;
    public Action IsGameEnd;
    public bool gamecheck = false;
    public bool gameplay = false;
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
                if (gamecheck && gameplay)
                {
                    IsGameStart?.Invoke();
                }
                else if (!gamecheck && gameplay)
                {
                    IsGameEnd?.Invoke();
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
    public int KilledChick = 0;
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
        SceneManager.sceneLoaded += GameManageInitialize;
    }
    void GameManageInitialize(Scene scene, LoadSceneMode mode)
    {
        playermove = FindObjectOfType<PlayerMove>();
        playerequiped = FindObjectOfType<Equiped>();
        playercam = FindObjectOfType<PlayerCam>();
        if ((playermove != null) && (playerequiped != null) && (playercam != null))
        {
            gameplay = true;
            GameCheck = false;
            Cursor.lockState = CursorLockMode.Locked;
            Timecode = WaitTime;
            TimeSys += timecount;
            IsGameStart += () =>
            {
                Timecode = timeset;
            };
            IsGameEnd += () =>
            {
                SceneController.Inst.LoadingActivate(3);
            };
        }
        else
        {
            gameplay = false;
            Cursor.lockState = CursorLockMode.None;
            TimeSys = (Timecode) => { };
            IsGameStart = null;
            IsGameEnd = null;
        }
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
}

