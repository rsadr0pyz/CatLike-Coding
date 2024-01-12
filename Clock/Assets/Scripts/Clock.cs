using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const float secondsToDegree = 6;
    private const float minutesToDegree = 6;
    private const float hoursToDegree = 30;

    //Clock's hands
    [SerializeField] 
    private Transform secondHandPivot, minuteHandPivot, hourHandPivot;



    private void Start()
    {
        SetClockHandsToTime(DateTime.Now);
    }

    private void Update()
    {
        SetClockHandsToTime(DateTime.Now);
    }

    private void SetClockHandsToTime(DateTime dateTime)
    {   
        TimeSpan time = dateTime.TimeOfDay;

        secondHandPivot.localRotation = Quaternion.Euler(0, secondsToDegree * (float) time.TotalSeconds ,0);
        minuteHandPivot.localRotation = Quaternion.Euler(0, minutesToDegree * (float) time.TotalMinutes ,0);
        hourHandPivot.localRotation = Quaternion.Euler(0, hoursToDegree * (float) time.TotalHours ,0);
    }
}
