using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAudio : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;

    [SerializeField]
    Slider slider;


    private void OnEnable()
    {
        slider.value = audio.volume;
        slider.onValueChanged.AddListener(SetSlider);
    }

    public void SetSlider(float volume)
    {
        audio.volume = volume;
    }

}
