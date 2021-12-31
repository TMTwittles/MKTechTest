using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : ColoredMenu
{
    [Header("UI Elements")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button accessibilityButton;
    [SerializeField] private Button quitButton;

    private String FormattedTime(float timeValue)
    {
        return TimeSpan.FromSeconds(timeValue).ToString(@"ss\:ff");
    }

    private void Awake()
    {
        newGameButton.onClick.AddListener(delegate { OnPressNewGame(); });
        accessibilityButton.onClick.AddListener(delegate { OnPressAccessibility(); });
        quitButton.onClick.AddListener(delegate {OnPressQuit();});
        InitializeColorGroups();
    }

    void OnEnable()
    {
        UpdateColorGroupsRandomly();
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
