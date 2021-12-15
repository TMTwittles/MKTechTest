using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private Button NewGameButton;

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
        
    }

    void OnPressNewGame()
    {
        GameManager.Instance.NewGame();
    }
}
