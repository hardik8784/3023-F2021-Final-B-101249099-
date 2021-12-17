/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 17,2021
 * File : ImportantDates.cs
 * Description : This is ImportantDates ScriptableObject
 * Revision History : v0.1 > Added On ScriptableObject so that Developers can modify the Date they want and implement into the Calendar
 *              
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateImportantDate/ Important Dates")]
public class ImportantDates : ScriptableObject
{
    public DateTime ImportantDate;
    public bool Yearly;
    public Sprite thumbnail;
    public string Description;
}
