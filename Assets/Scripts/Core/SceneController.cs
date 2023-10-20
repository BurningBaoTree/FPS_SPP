using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    string StartScene = "StartScene";
    string LoadingScene = "LoadScene";
    string mainScenes = "mainScenes";
    string EndScene = "EndScene";

    bool loadingState = false;

    public AsyncOperation async;

    public void LoadingActivate(int sellect)
    {
        switch (sellect)
        {
            case 0:
                StartCoroutine(LoadSene(StartScene));
                break;
            case 1:
                StartCoroutine(LoadSene(LoadingScene));
                break;
            case 2:
                StartCoroutine(LoadSene(mainScenes));
                break;
            case 3:
                StartCoroutine(LoadSene(EndScene));
                break;
            default: break;
        }
    }

    IEnumerator LoadSene(string nextScene)
    {
        //다음 씬 로드
        async = SceneManager.LoadSceneAsync(nextScene);

        //씬 접근 거부
        async.allowSceneActivation = false;

        //로드 상태 체크 반복문 종료시 다음 명령 시행
        while (async.progress < 0.9f)
        {
            yield return null;
        }

        //로드 상태 끝
        loadingState = true;

        //로드가 끝났음에 따라 아래 코드 실행
        if (loadingState)
        {
            //0.5초 대기후
            yield return new WaitForSeconds(0.5f);

            //씬 접근 허가
            async.allowSceneActivation = true;
        }
    }
}
