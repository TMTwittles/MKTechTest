using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text displayText;
    [SerializeField] private List<GameObject> optionButtons;
    private Text[] optionButtonTexts; // Faster access to option button text

    [Header("Game Data")] 
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameData gameData;
    [SerializeField] private ColorDataList colorDataList;
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

        if (playerData.NumAttempts >= gameData.TotalAttempts)
        {
            playerData.AddAttempt(successfulAttempt, stopWatch.ElapsedTime);
            GameManager.Instance.ShowResults();
        }
        else
        {
            playerData.AddAttempt(successfulAttempt);
            Shuffle(); // Randomise the display and options buttons once a selection has been made.
        }
    }

    void Shuffle()
    {
        // Assign the correct color to the display button
        correctColor = colorDataList.GetRandomColorName(true);
        displayText.text = correctColor;
        Color displayColor = colorDataList.GetRandomColor(correctColor, 0, false);
        displayText.color = new Color(displayColor.r, displayColor.g, displayColor.b);

        // Place the correct color at the start of the array and shuffle it, to ensure randomness for option buttons
        colorDataList.GetRandomColorName(correctColor, 0, true);

        // Assign colors to the option buttons
        string previousButtonName = "";
        Color newButtonColor = Color.black;
        int offset = 1; // As the above line places the correct option at 0, an offset of 1 must be in place to not replace the correct color.
        int iter = 0;
        int correctButtonIteration = Random.Range(0, optionButtonTexts.Length);

        foreach (Text optionButton in optionButtonTexts)
        {
            if (iter != correctButtonIteration)
            {
                if (iter == 0)
                {
                    previousButtonName = colorDataList.GetRandomColorName(0, false); // Ignore first index as the correct color is placed there.
                    newButtonColor = colorDataList.GetRandomColor(0, false);
                }
                else
                {
                    // Start from the offset (to ignore correct color) and iterator, this ensures a button text only uses colors that have not been assigned yet. 
                    previousButtonName = colorDataList.GetRandomColorName(previousButtonName, offset + (iter - 1), false);
                    newButtonColor = colorDataList.GetRandomColor(offset + (iter - 1), false);
                }

                optionButton.text = previousButtonName;
                optionButton.color = new Color(newButtonColor.r, newButtonColor.g, newButtonColor.b);
            }
            else
            {
                newButtonColor = colorDataList.GetRandomColor(0, false);

                // Apply correct color as it is the correct iteration.
                optionButton.text = correctColor;
                optionButton.color = new Color(newButtonColor.r, newButtonColor.g, newButtonColor.b);
            }

            iter += 1;
        }
    }

    


}
