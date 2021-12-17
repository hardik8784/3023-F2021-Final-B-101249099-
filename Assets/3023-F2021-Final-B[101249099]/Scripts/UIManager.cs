/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 14,2021
 * File : UIManager.cs
 * Description : This is UI Manager Script
 * Revision History : v0.1 > Added the Delegate, Changing the Updated Time on Screen           
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI CurrentTimeText;
    public TextMeshProUGUI DateText, TimeText, SeasonText, WeekText;



    private void OnEnable()
    {
        //WeatherManager.OnWeatherChange += WeatherChanged;
        // GameManager.OnTick += Tick;
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }
    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;        //Subscribe to OnDateTimeChanged Event
        //WeatherManager.OnWeatherChange -= WeatherChanged;
      //  GameManager.OnTick -= Tick;
    }

    private void UpdateDateTime(DateTime dateTime)              //Passing dateTime into function
    {
        DateText.text = dateTime.Day.ToString() + " " + dateTime.Date.ToString("D2") + "/" + dateTime.Year.ToString("D2") ;        
        TimeText.text = dateTime.TimeToString();
        SeasonText.text = dateTime.Season.ToString();
        WeekText.text = "Week : " + dateTime.CurrentWeek;
    }

    private void Tick()
    {
     //   CurrentTimeText.text = "Time :" + GameManager.CurrentTick;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //    CurrentTimeText.text = "Time : " + GameManager.CurrentGameTime.ToString();
    }

    //void WeatherChanged(Weather CurrentWeather, Queue<Weather> WeatherQueue)
    //{
        
    //}
}
