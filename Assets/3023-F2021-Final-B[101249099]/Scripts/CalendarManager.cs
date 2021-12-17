
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
