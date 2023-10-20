using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSlider : MonoBehaviour
{
    SceneController scene;
    Slider slider;
    private void Awake()
    {
        slider = transform.GetComponent<Slider>();
    }
    private void OnEnable()
    {
        scene = SceneController.Inst;
        scene.LoadingActivate(2);
    }
    private void Update()
    {
        slider.value = scene.async.progress;
    }
}
