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
    [Header("UI")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text displayText;
    [SerializeField] private List<GameObject> optionButtons;
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
        playerData.ResetResults();
        stopWatch.TurnOn();
        randomColorsTextOnly.Reset();
        visualRandomColors.Reset();
        visualRandomColors.IgnoreColor(GameManager.Instance.BackgroundColorData.ColorName);
        Shuffle();
    }

    void Update()
    {
        timerText.text = stopWatch.GetTimeFormatted();
    }

    // Method determines if the player selected the right color
    private void OptionButtonPressed(string selectedColor)
    {
        successfulAttempt = selectedColor == correctColor;

        visualRandomColors.Reset(); // Reset all visual colors
        visualRandomColors.IgnoreColor(selectedColor); // Ignore the color selected by the player as this will be the background color
        GameManager.Instance.UpdateBackgroundColor(visualRandomColors.GetColorData(selectedColor));

        if (playerData.NumAttempts >= gameData.TotalAttempts)
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
        visualRandomColors.IgnoreColor(correctColor);

        string randomName = "";
        ColorData randomColorData = null;
        int iter = 0;
        int correctButtonIteration = Random.Range(0, optionButtonTexts.Length);

        // Apply values to the display button
        displayText.text = correctColor;
        randomColorData = visualRandomColors.GetRandomColor();
        displayText.color = Helper.GenColor(randomColorData.ColorRGB);

        visualRandomColors.IgnoreColor(randomColorData.ColorName);
        
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
            randomColorData = visualRandomColors.GetRandomColor();
            visualRandomColors.IgnoreColor(randomColorData.ColorName);
            optionButton.color = Helper.GenColor(randomColorData.ColorRGB);

            iter += 1;
        }
        
        // Once option buttons have been assigned, apply a random remaining color to the timer text
        timerText.color = Helper.GenColor(visualRandomColors.GetRandomColor().ColorRGB);
    }

    


}
