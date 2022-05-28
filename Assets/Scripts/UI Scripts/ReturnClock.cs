using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ReturnClock : MonoBehaviour
{
    private Text textClock;
    private DateTime time;
    private string hour;
    private string minute;

    void Start()
    {
        textClock = GetComponent<Text>();
    }
    void Update()
    {
        time = DateTime.Now;
        hour = LeadingZero(time.Hour);
        minute = LeadingZero(time.Minute);
        textClock.text = hour + ":" + minute;
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}