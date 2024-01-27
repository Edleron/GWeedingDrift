using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI puanner;

    private void OnEnable()
    {
        EventManager.onDetectSlider += UpdateSliderValue;
        EventManager.onDetectTimer  += UpdateTimerText;
        EventManager.onDetectPuan   += UpdatePuanText;
    }

    private void OnDisable()
    {
        EventManager.onDetectSlider -= UpdateSliderValue;
        EventManager.onDetectTimer  -= UpdateTimerText;
        EventManager.onDetectPuan   -= UpdatePuanText;
    }

    public 
    void Start()
    {
        // slider?.onValueChanged.AddListener(delegate { UpdateSliderValue(); });

        RestartSlider();
    }

    void Update()
    {
        
    }

    private void RestartSlider() 
    {
        slider.minValue = 0;
        slider.value = 0;
        slider.maxValue = LevelManager.Instance.GetLevelProperty();
    }

    public void UpdateSliderValue(int value)
    {
        slider.value = slider.value + 1;

        if (slider.value == slider.maxValue)
        {
            RestartSlider();
            EventManager.Fire_onDetectRestart();
            LevelManager.Instance.SetLevel();
            LevelManager.Instance.puanValue++;
            UpdatePuanText(LevelManager.Instance.puanValue);
            UpdateTimerText(0);
        }
    }

    public void UpdatePuanText(int value) {
        puanner.text = "Puan : " + " " + value.ToString();
    }

    public void UpdateTimerText(int value) {
        timer.text = "Timer : " + " " + value.ToString();
    }
}
