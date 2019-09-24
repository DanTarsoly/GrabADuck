using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public int MaxSeconds;
    public Text TimeText;
    public Color StartColor;
    public Color EndColor;
    public float SecondsLeft { get; private set; }
    public bool IsOver { get; private set; }
    public bool IsStopped { get; private set; }
    
    void Awake()
    {
        ReSet();
    }

    void Update()
    {
        if (IsOver || IsStopped)
            return;

        if (SecondsLeft <= 0)
        {
            IsOver = true;
            Stop();
            return;
        }
        
        SecondsLeft -= Time.deltaTime;
        UpdateTimeText();
    }

    public void ReStart()
    {
        ReSet();
        Continue();
    }

    public void ReSet()
    {
        IsOver = false;
        IsStopped = true;
        SecondsLeft = MaxSeconds;
        UpdateTimeText();
    }

    public void Continue()
    {
        IsStopped = false;
    }
    
    public void Stop()
    {
        IsStopped = true;
    }

    private void UpdateTimeText()
    {
        int secondsLeft = Mathf.CeilToInt(SecondsLeft) % 60;
        int minutesLeft = Mathf.CeilToInt(SecondsLeft) / 60;
        TimeText.text = minutesLeft.ToString("D2") + " : " + secondsLeft.ToString("D2");
        TimeText.color = Color.Lerp(EndColor, StartColor, SecondsLeft / MaxSeconds);
    }
}
