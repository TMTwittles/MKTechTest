using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    [SerializeField] private Text textUI; // Stopwatch UI, this is placeholder
    private bool active;
    private float timeOffset; // Offset for Time.time to reach 0.0f;
    private float elapsedTime = 0.0f;
    
    public void TurnOn()
    {
        timeOffset = Time.time;
        active = true;
    }

    void Update()
    {
        if (active)
        {
            elapsedTime = Time.time - timeOffset;

            // TODO: Create a new class to update stopwatch UI, StopWatch class should only worry about time, not UI
            textUI.text = TimeSpan.FromSeconds(elapsedTime).ToString(@"ss\:ff");
        }
    }
}
