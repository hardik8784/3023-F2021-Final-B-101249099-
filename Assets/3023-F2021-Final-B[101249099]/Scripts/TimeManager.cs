/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 17,2021
 * File : TimeManager.cs
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
    public int TickMinutesIncreased = 10 ;        //EveryTick instead of 1 seconds, increase it by 10, Developer can change this.
    public float TimeBetweenTicks = 1;           //Time between Tick can be change by Developer
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
        //DateTime.AdvanceMinutes(10);
        

        CurrentTimeBetweenTicks += Time.deltaTime;
        

        if (CurrentTimeBetweenTicks >= TimeBetweenTicks)
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
        DateTime.AdvanceMinutes(TickMinutesIncreased);
        OnDateTimeChanged?.Invoke(DateTime);
    }
  
}

[System.Serializable]
public struct DateTime
{
    #region Fields
    private Days day;
    [SerializeField]
    private int date;
    [SerializeField]
    private int year;
    [SerializeField]
    private int hour;
    [SerializeField]
    private int minutes;
    [SerializeField]
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
    public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;
    #endregion

    #region Constructors
    public DateTime(int date, int season, int year, int hour, int minutes)
    {
        this.day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
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
        totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));


        totalNumWeeks = 1 + totalNumDays / 7;
    }
    #endregion

    #region TimeAdvancement

    public void AdvanceMinutes(int SecondsToAdvanceBy)
    {
        //Debug.Log("Day : Hours : Minutes : " + date + ":" + hour + ":" + minutes);

        if (minutes + SecondsToAdvanceBy >= 60)
        {
            minutes = (minutes + SecondsToAdvanceBy) % 60;
            AdvanceHour();

        }
        else
        {
            minutes += SecondsToAdvanceBy;

        }
    }

    private void AdvanceHour()
    {
        if ((hour + 1) == 24)
        {
            hour = 0;
            AdvanceDay();
        }
        else
        {
            hour++;
        }
    }

    private void AdvanceDay()
    {
        day++;

        if (day > (Days)7)
        {
            day = (Days)1;
            totalNumWeeks++;
        }

        date++;

        if (date % 29 == 0)                   //As we have 28 days in a month
        {
            AdvanceSeason();
            date = 1;
        }
        totalNumDays++;
    }

    private void AdvanceSeason()
    {
        if (Season == Season.Winter)         //If in Last Season
        {
            season = Season.Spring;         //Turn Back to First Season of Another(Next) Year
            AdvanceYear();
        }
        else
        {
            season++;
        }
    }

    private void AdvanceYear()
    {
        date = 1;                           //Switch back the First date of the First Season
        year++;
    }
    #endregion

    #region Bool Checks

    public bool isNight()
    {
        return hour > 18 || hour < 6;       //If Time is 6 PM || 6 AM
    }

    public bool isMorning()
    {
        return hour >= 6 && hour <= 12;     // If Time is > 6 AM && < 12 PM
    }
    public bool isAfternoon()
    {
        return hour > 12 && hour < 18;      //If Time is >12 PM && < 6 PM
    }

    public bool isWeekend()
    {
        return day > Days.Fri ? true : false;
    }

    public bool isParticularDay(Days _day)
    {
        return day == _day;
    }

    #endregion
    #region Important Dates

    public DateTime NewYearDay(int year)
    {
        if (year == 0)
            year = 1;
        //Debug.Log("New Year Day Arrived");
        return new DateTime(1, 0, year, 0, 0);
    }

    #endregion

    

    #region ToString

    public string TimeToString()
    {
        int AdjustedHour = 0;

        if(hour == 0)
        {
            AdjustedHour = 12;
        }
        else if(hour >= 13)
        {
            AdjustedHour = hour - 12;
        }
        else
        {
            AdjustedHour = hour;
        }

        string AMPM = hour == 0 || hour < 12 ? "AM" : "PM";

        return AdjustedHour.ToString() + " : " + minutes.ToString("D2") + "   " + AMPM;

    }

   
    #endregion
}

[System.Serializable]
    public enum Days
    {
        NULL = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 5,
        Sat = 6,
        Sun = 7

    }

    [System.Serializable]
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Autumn = 2,
        Winter =3
    }


