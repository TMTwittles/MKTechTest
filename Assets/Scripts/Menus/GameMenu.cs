using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMenu : Menu
{
    [Header("UI Elements")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text displayText;
    [SerializeField] private List<GameObject> optionButtons;
    [SerializeField] private Button pauseButton;
    private Text[] optionButtonTexts; // Faster access to option button text

    [Header("Game Data")] 
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameData gameData;
    [SerializeField] private RandomColors randomColorsTextOnly; // This will only modify the text of the buttons
    private StopWatch stopWatch;
    private string correctColor;
    private bool successfulAttempt = false;

    void Awake()
    {
        pauseButton.onClick.AddListener(delegate {OnPressPause();});
        
        stopWatch = GetComponent<StopWatch>();
        optionButtonTexts = new Text[optionButtons.Count];
        int iter = 0;
        foreach (GameObject optionButton in optionButtons)
        {
            optionButton.GetComponent<OptionButton>().optionButtonPressed += OptionButtonPressed;
            optionButtonTexts[iter] = optionButton.GetComponent<Text>();
            iter += 1;
        }
    }

    void OnEnable()
    {
        stopWatch.TurnOn();
    }

    public override void Reset()
    {
        playerData.ResetResults();
        stopWatch.Reset();
        stopWatch.TurnOn();
        randomColorsTextOnly.Reset();
        ResetRandomColors();
        Shuffle();
    }

    void Update()
    {
        timerText.text = stopWatch.GetTimeFormatted();
    }

    private void OnPressPause()
    {
        stopWatch.Pause();
        GameManager.Instance.SetActiveMenu(MenuID.PauseMenu, false);
    }

    // Method determines if the player selected the right color
    private void OptionButtonPressed(string selectedColor)
    {
        successfulAttempt = selectedColor == correctColor;

        if (data.EnableDynamicBackground)
        {
            data.CustomRandomColors.Reset(); // Reset all visual colors
            data.CustomRandomColors.IgnoreColor(selectedColor); // Ignore the color selected by the player as this will be the background color
            GameManager.Instance.UpdateBackgroundColor(data.CustomRandomColors.GetColorData(selectedColor));
        }
        else
        {
            ResetRandomColors();
        }
        
        if (playerData.NumAttempts >= gameData.TotalAttempts - 1)
        {
            playerData.AddAttempt(successfulAttempt, stopWatch.ElapsedTime);
            GameManager.Instance.ShowResults();
        }
        else
        {
            playerData.AddAttempt(successfulAttempt);
            randomColorsTextOnly.Reset(); // Reset all text colors
            Shuffle(); // Randomize the display and option buttons as a selection has been made.
        }
    }

    private void Shuffle()
    {
        // Assign a new color to the display button, this will be the correct answer
        correctColor = randomColorsTextOnly.GetRandomColor().ColorName;

        // Ignore the correct color from all random color options
        randomColorsTextOnly.IgnoreColor(correctColor); 
        data.CustomRandomColors.IgnoreColor(correctColor);

        string randomName = "";
        ColorData randomColorData = null;
        int iter = 0;
        int correctButtonIteration = Random.Range(0, optionButtonTexts.Length);

        // Apply values to the display button
        displayText.text = correctColor;
        randomColorData = data.CustomRandomColors.GetRandomColor();
        displayText.color = randomColorData.ColorRGB;

        data.CustomRandomColors.IgnoreColor(randomColorData.ColorName);
        
        // Apply new options to each option button in the menu
        foreach (Text optionButton in optionButtonTexts)
        {
            if (iter != correctButtonIteration)
            {
                randomName = randomColorsTextOnly.GetRandomColor().ColorName;
                randomColorsTextOnly.IgnoreColor(randomName);
                optionButton.text = randomName;
            }
            else
            {
                optionButton.text = correctColor; // Apply correct color as it is the correct iteration.
            }

            // Apply a random color from the pool of remaining visual colors
            randomColorData = data.CustomRandomColors.GetRandomColor();
            data.CustomRandomColors.IgnoreColor(randomColorData.ColorName);
            optionButton.color = randomColorData.ColorRGB;

            iter += 1;
        }
        
        // Once option buttons have been assigned, apply a random remaining color to the timer text
        timerText.color = data.CustomRandomColors.GetRandomColor().ColorRGB;
        pauseButton.gameObject.GetComponent<Text>().color = data.CustomRandomColors.GetRandomColor().ColorRGB;
    }

    


}
