using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int successfulAttempts = 0;

    public int SuccessfulAttempts
    {
        get { return successfulAttempts; }
    }

    [SerializeField] private int failedAttempts = 0;

    public int FailedAttempts
    {
        get { return failedAttempts; }
    }

    [SerializeField] private float totalTime = 0.0f;

    public float TotalTime
    {
        get { return totalTime; }
    }

    [SerializeField] private int numAttempts = 0;

    public int NumAttempts
    {
        get { return numAttempts; }
    }

    public void AddAttempt(bool success)
    {
        if (success)
            successfulAttempts += 1;
        else
            failedAttempts += 1;

        numAttempts += 1;
    }

    public void AddAttempt(bool success, float _totalTime)
    {
        AddAttempt(success);
        totalTime = _totalTime;
    }

    public void ResetResults()
    {
        successfulAttempts = 0;
        failedAttempts = 0;
        numAttempts = 0;
        totalTime = 0.0f;
    }
}
