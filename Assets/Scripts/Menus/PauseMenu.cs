using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : ColoredMenu
{
    [Header("UI Elements")] 
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button accessibilityButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        resumeButton.onClick.AddListener(delegate {OnPressResume();});
        retryButton.onClick.AddListener(delegate {OnPressRetry();});
        accessibilityButton.onClick.AddListener(delegate{OnPressAccessibility();});
        InitializeColorGroups();
    }

    void OnEnable()
    {
        ResetRandomColors();
        UpdateColorGroupsRandomly();
    }

    void OnPressResume()
    {
        GameManager.Instance.SetActiveMenu(MenuID.GameMenu, true);
    }

    void OnPressRetry()
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
