/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 17,2021
 * File : CalendarPanel.cs
 * Description : This is CalendarPanel Script which is attached to each day in the Calendar(Major functionality which is not needed for others will do here)
 * Revision History : v0.1 > 
 *              
 */


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class CalendarPanel : MonoBehaviour
{
    public TextMeshProUGUI DateText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpDate(string date)
    {
        //DateTime = null;
        DateText.text = date;
    }
}