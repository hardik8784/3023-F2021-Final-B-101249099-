using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentTimeText;

    private void OnEnable()
    {
        //WeatherManager.OnWeatherChange += WeatherChanged;
        GameManager.OnTick += Tick;
    }
    private void OnDisable()
    {
        //WeatherManager.OnWeatherChange -= WeatherChanged;
        GameManager.OnTick -= Tick;
    }


    private void Tick()
    {
        CurrentTimeText.text = "Tick :" + GameManager.CurrentTick;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTimeText.text = "Time : " + GameManager.CurrentGameTime.ToString().ToUpper();
    }

    //void WeatherChanged(Weather CurrentWeather, Queue<Weather> WeatherQueue)
    //{
        
    //}
}
