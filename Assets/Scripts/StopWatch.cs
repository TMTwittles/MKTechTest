using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    private bool active;
    private bool paused = false;
    private float timeOffset; // Offset for Time.time to reach 0.0f;
    private float elapsedTime = 0.0f;

    public float ElapsedTime
    {
        get { return elapsedTime; }
    }

    public void TurnOn()
    {
        if (!paused)
            timeOffset = Time.time;
        paused = false;
        active = true;
    }

    public void Reset()
    {
        elapsedTime = 0.0f;
        timeOffset = 0.0f;
        active = false;
        paused = false;
    }

    public void Pause()
    {
        active = false;
        paused = true;
    }

    void Update()
    {
        if (active)
        {
            elapsedTime = Time.time - timeOffset;
        }
    }

    public string GetTimeFormatted()
    {
        return TimeSpan.FromSeconds(elapsedTime).ToString(@"ss\:ff");
    }
}
