using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenu : MonoBehaviour
{

    [Header("UI Elements")] 
    [SerializeField] private Text numSuccessfulAttempts;
    [SerializeField] private Text numFailedAttempts;
    [SerializeField] private Text numAverageAttemptTime;
    [SerializeField] private Text numTotalTime;
    [SerializeField] private Button NewGameButton;

    [Header("Game Data")]
    [SerializeField] private PlayerData playerData;

    private String FormattedTime(float timeValue)
    {
        return TimeSpan.FromSeconds(timeValue).ToString(@"ss\:ff");
    }

    void Awake()
    {
        NewGameButton.onClick.AddListener(delegate { OnPressNewGame(); });
    }

    void OnEnable()
    {
        UpdateResults();
    }

    // Results are update based off player data
    void UpdateResults()
    {
        numSuccessfulAttempts.text = playerData.SuccessfulAttempts.ToString();
        numFailedAttempts.text = playerData.FailedAttempts.ToString();
        numAverageAttemptTime.text = FormattedTime(playerData.TotalTime / playerData.NumAttempts);
        numTotalTime.text = FormattedTime(playerData.TotalTime);
    }

    void OnPressNewGame()
    {
        playerData.ResetResults();
        GameManager.Instance.NewGame();
    }
}
