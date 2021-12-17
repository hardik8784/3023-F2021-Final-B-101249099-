/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 17,2021
 * File : UIManager.cs
 * Description : This is Time Manager Script
 * Revision History : v0.1 > Added Basic required Functionality of a Calender
 *              
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Date & Time Settings")]
    [Range(1, 28)]                              //Calender has 28 slots to fill in so 28 Days per Season
    public int dateInMonth;
    [Range(1, 4)]                               // Minimum 4 Season Required
    public int season;
    [Range(1, 99)]
    public int year;                            // This will be the Year
    [Range(0, 24)]
    public int hour;                            //There is 24 Hours in a Day
    [Range(0, 6)]                               
    public int minutes;                         // We will multiplay this with 10 so putting 0 to 6 here, to speed up the time, Developer can change it from Editor
    private DateTime DateTime;                  

    [Header("Tick Settings")]
    public int TickSecondsIncrease = 10;        //EveryTick instead of 1 seconds, increase it by 10, Developer can change this.
    public float TimeBetweenTicks = 1;          //Time between Tick can be change by Developer
    private float CurrentTimeBetweenTicks = 0;      

    public static UnityAction<DateTime> OnDateTimeChanged;

    private void Awake()
    {
        DateTime = new DateTime(dateInMonth, season - 1, year, hour, minutes * 10);
    }

    // Start is called before the first frame update
    void Start()
    {
       OnDateTimeChanged?.Invoke(DateTime);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTimeBetweenTicks += Time.deltaTime;
        Debug.Log("Minutes" + minutes );

        if (CurrentTimeBetweenTicks >= TickSecondsIncrease)
        {
            CurrentTimeBetweenTicks = 0;
            Tick();
        }
    }

    void Tick()
    {
        AdvanceTime();
    }

    void AdvanceTime()
    {
        DateTime.AdvanceMinutes(TickSecondsIncrease);
        OnDateTimeChanged?.Invoke(DateTime);
    }
  
}

[System.Serializable]
public struct DateTime
{
    #region Fields
    private Days day;
    private int date;
    private int year;

    private int hour;
    private int minutes;

    private Season season;

    private int totalNumDays;
    private int totalNumWeeks;
    #endregion

    #region Properties

    public Days Day => day;

    public int Date => date;
    public int Hour => hour;
    public int Minutes => minutes;
    public Season Season => season;
    public int Year => year;
    public int TotalNumDays => totalNumDays;
    public int TotalNumWeeks => totalNumWeeks;


    /// <summary>
    /// If We have to Select the Current Week we can do like that
    /// We have 4 Seasons in a Year,each Month has 4 weeks, so 4 *4 =16 Weeks we have in total in Year
    /// so, for the CurrentWeek we can find the TotalNumber of Week % Total week in year 
    /// If we found % is 0 then it will be 16 weeks otherwise the answer
    /// </summary>
    public int CurrentWeek => TotalNumWeeks % 16 == 0 ? 16 : TotalNumWeeks % 16;
    #endregion

    #region Constructors
    public DateTime(int date, int season, int year, int hour, int minutes)
    {
        this.day = (Days)(date % 7);
        if(day ==0)  day = (Days)7;
        this.date = date;
        this.season = (Season)season;
        this.year = year;

        this.hour = hour;
        this.minutes = minutes;

        // To Calculate Total Number of Days 
        // We have to first find the date
        // we have 28 days in a month and 4 season
        // For a Calender Year it will be 112 Days in total(28 *4 = 112)
        // let's say we are in 2nd Season 4th Day
        // assume it's first year
        // so TotalNumber of Day  = 4 + (28 * (2)) + ( 112 * (1-1)) => 60 Days!!!
        totalNumDays = date + (28 * (int)this.season) + (112 * ( year -1));


        totalNumWeeks = 1 + totalNumDays / 7;
    }
    #endregion

    #region TimeAdvancement

    public void AdvanceMinutes(int SecondsToAdvanceBy)
    {
        if(minutes + SecondsToAdvanceBy >= 60)
        {
            minutes = (minutes + SecondsToAdvanceBy) % 60;
            AdvanceHour();
            
        }
        else
        {
            minutes += SecondsToAdvanceBy;
            Debug.Log("Minutes" + minutes);
        }
    }

    private void AdvanceHour()
    {
        //TODO: 
        //Implement the hour feature
        //If more then 24 then change the Day
        //Increment the Hours
    }
    #endregion
}

    [System.Serializable]
    public enum Days
    {

    }

    [System.Serializable]
    public enum Season
    {
    Spring = 0,
    Summer = 1,
    Autumn = 2,
    Winter =3
    }


