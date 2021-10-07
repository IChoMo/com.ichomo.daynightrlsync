using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightRLSync : MonoBehaviour
{
    public Text LocationAndTimeText;

    private string AMPM;
    private bool AMPMBool;
    private int CurrentHour;
    private float CurrentMinute;

    public GameObject Sun;
    private Vector3 eulerRotation;
    public float SunRiseTime = 8;
    public float SunSetTime = 8;
    private float TimeIntoDay;
    private float TotalDayLength;

    public int mySelectedRegion;
    public string[] RegionLocationLabels; //The name of the city or state or country you want to see the time for
    public int[] RegionTimeZones; //Plus or Minus Utc/Gmt time ex. PDT time = gmt -7 so in the region write: -7

    void Start()
    {
        //sets the initial sun rotation to its current rotation
        eulerRotation = Sun.transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        //calls time refresh once per frame
        UpateRegionTime();
    }

    void UpateRegionTime()
    {
        //sets defult time to UTC/GMT time
        AMPM = System.DateTime.UtcNow.ToString("tt");
        CurrentHour = System.DateTime.UtcNow.Hour;

        //takes the UTC/GMT time and adds your chosen timezone
        if (CurrentHour > 12)
        {
            //Keep the clock at 12 hour time
            CurrentHour = CurrentHour - 12 + RegionTimeZones[mySelectedRegion];
        }
        else
        {
            CurrentHour += RegionTimeZones[mySelectedRegion];
        }

        CurrentMinute = System.DateTime.UtcNow.Minute;

        //sets AMPM bool to be the same as the string
        //AMPM BOOL     AM = TRUE    PM = false
        if (AMPM == "AM")
        {
            AMPMBool = true;
        }
        if (AMPM == "PM")
        {
            AMPMBool = false;
        }

        //if the time zone is behind UTC/GMT time the converstion to 12 hours time may result in a negitive number or 0:00, this corrects that. By passing 12:00 you switch your AM/PM
        if (CurrentHour <= 0)
        {
            CurrentHour = 12 + CurrentHour;
            AMPMBool = !AMPMBool;
        }
        //if its morning set AM if its evening set PM
        if (CurrentHour > 12)
        {
            CurrentHour -= 12;
            AMPMBool = !AMPMBool;
        }
        if (CurrentHour == 12)
        {
            AMPMBool = !AMPMBool;
        }

        if (AMPMBool == true)
        {
            AMPM = "AM";
        }
        if (AMPMBool == false)
        {
            AMPM = "PM";
        }

        /* You can use this if you have a time zone like Chennai which needs gmt + 5 hours & 30 minuets just specify the spesific region by replace the # below
         
        if (mySelectedRegion == #)
        {
            CurrentMinuet += 30;
        }*/

        //set the text to say the name of your location and the current time of that region
        LocationAndTimeText.text = string.Format("{0} {1}:{2:00} {3}", RegionLocationLabels[mySelectedRegion], CurrentHour, CurrentMinute, AMPM);
        SunTime(CurrentHour, CurrentMinute, AMPMBool);
    }

    void SunTime(int Hour, float Minuet, bool tt)
    {
        //depending on when the sunrise and sunset is, will determin how long the day is
        TotalDayLength = (12 - SunRiseTime) + SunSetTime;

        //find how far we into the solar day based on the current time and the time of sunrise / sunset
        if (tt) //tt = AMPM bool
        {
            if (Hour == 12)
            {
                TimeIntoDay = (12 - SunRiseTime) + Hour + Minuet / 60;
            }
            else
            {
                TimeIntoDay = (Hour - SunRiseTime) + Minuet / 60;
            }
        }
        else
        {
            if (Hour == 12)
            {
                TimeIntoDay = (Hour - SunRiseTime) + Minuet / 60;
            }
            else
            {
                TimeIntoDay = (12 - SunRiseTime) + Hour + Minuet / 60;
            }
        }

        //sets sun rotation based on 185 degrees of rotation (I find works nicely for unity's Directional light) and the percentage of time into the Solarday
        Sun.transform.rotation = Quaternion.Euler(TimeIntoDay * 185 / TotalDayLength, eulerRotation.y, eulerRotation.z);
    }
}
