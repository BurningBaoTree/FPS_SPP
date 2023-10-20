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
        //���� �� �ε�
        async = SceneManager.LoadSceneAsync(nextScene);

        //�� ���� �ź�
        async.allowSceneActivation = false;

        //�ε� ���� üũ �ݺ��� ����� ���� ��� ����
        while (async.progress < 0.9f)
        {
            yield return null;
        }

        //�ε� ���� ��
        loadingState = true;

        //�ε尡 �������� ���� �Ʒ� �ڵ� ����
        if (loadingState)
        {
            //0.5�� �����
            yield return new WaitForSeconds(0.5f);

            //�� ���� �㰡
            async.allowSceneActivation = true;
        }
    }
}
