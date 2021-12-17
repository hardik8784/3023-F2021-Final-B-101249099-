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
