using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : ColoredMenu
{
    [Header("UI Elements")]
    [SerializeField] private Button NewGameButton;

    private String FormattedTime(float timeValue)
    {
        return TimeSpan.FromSeconds(timeValue).ToString(@"ss\:ff");
    }

    private void Awake()
    {
        NewGameButton.onClick.AddListener(delegate { OnPressNewGame(); });
        InitializeColorGroups();
    }

    void OnEnable()
    {
        UpdateColors();
    }

    void OnPressNewGame()
    {
        ResetVisualRandomColors();
        GameManager.Instance.NewGame();
    }
}
