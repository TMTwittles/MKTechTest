using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenu : ColoredMenu
{
    [Header("UI Elements")]
    [SerializeField] private Text numSuccessfulAttempts;
    [SerializeField] private Text numFailedAttempts;
    [SerializeField] private Text numAverageAttemptTime;
    [SerializeField] private Text numTotalTime;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button accessibilityButton;
    [SerializeField] private Button quitButton;
    
    [Header("Game Data")]
    [SerializeField] private PlayerData playerData;

    private String FormattedTime(float timeValue)
    {
        return TimeSpan.FromSeconds(timeValue).ToString(@"ss\:ff");
    }

    void Awake()
    {
        newGameButton.onClick.AddListener(delegate { OnPressNewGame(); });
        accessibilityButton.onClick.AddListener(delegate{OnPressAccessibility();});
        quitButton.onClick.AddListener(delegate{OnPressQuit();});
        InitializeColorGroups();
    }

    void OnEnable()
    {
        UpdateResults();
        UpdateColorGroupsRandomly();
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
        GameManager.Instance.NewGame(true);
    }

    void OnPressAccessibility()
    {
        GameManager.Instance.SetActiveMenu(MenuID.AccessibilityMenu, false);
    }

    void OnPressQuit()
    {
        GameManager.Instance.Quit();
    }
}
