/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 17,2021
 * File : CalendarManager.cs
 * Description : This is CalendarManager Script
 * Revision History : v0.1 > Added basic variables and Delegates
 *              
 */


using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


public class CalendarManager : MonoBehaviour
{
    public List<CalendarPanel> calenderPanels;
    public List<ImportantDates> importantDates;
    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI setDescriptionText;
    public static TextMeshProUGUI DescriptionText;

    public TimeManager _TimeManagerRefrence;

    private int currentSeasonView = 0;
    private DateTime previousDateTime;

    private void Awake()
    {
        TimeManager.OnDateTimeChanged += DateTimeChanged;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= DateTimeChanged;
    }

    private void DateTimeChanged(DateTime dateTime)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        DescriptionText = setDescriptionText;
        DescriptionText.text = " ";
        previousDateTime = _TimeManagerRefrence.DateTime;
        
        SortDates();
        FillPanels((Season)0);
    }

    private void SortDates()
    {
       importantDates = importantDates.OrderBy(d => d.ImportantDate.Season).ThenBy(d => d.ImportantDate.Date).ToList();
    }

    private void FillPanels(Season _season)
    {
        seasonText.text = _season.ToString();

        for(int i = 0; i < calenderPanels.Count ; i++)
        {
            calenderPanels[i].SetUpDate((i + 1).ToString());

            foreach(var date in importantDates)
            {
                if((i+1) == date.ImportantDate.Date && date.ImportantDate.Season == _season)
                {
                    calenderPanels[i].AssignImportantDate(date);
                }
            }
            //}
            //TODO:
            //Step 1: Input every seaaon with button press
            //Step 2: Fill in the Important Dates
            //Step 3: Image allocating the Important Dates
            //Step 4: Highlight on the Date
        }
    }

    public void OnNextSeason()
    {
        currentSeasonView += 1;
        if (currentSeasonView > 3)
        {
            currentSeasonView = 0;
        }
        FillPanels((Season)currentSeasonView);
    }

    public void OnPreviousSeason()
    {
        currentSeasonView -= 1;
        if (currentSeasonView < 0)
        {
            currentSeasonView = 3;
        }
        FillPanels((Season)currentSeasonView);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
