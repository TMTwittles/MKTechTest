using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private StopWatch stopWatch;
    private ColorButton colorButton;

    [SerializeField] private ColorDataList colorDataList; 
    [SerializeField] private List<UIButton> optionButtons; // These are the options player have to guess color.

    private void Start()
    {
        colorButton = GetComponent<ColorButton>();
        stopWatch = GetComponent<StopWatch>();
        stopWatch.TurnOn();
    }

    // Subscribed to an option button, invoked by option button when pressed
    public void OptionButtonPressed()
    {
        colorButton.ChangeText();
        ChangeOptionButtons();
        Debug.Log(colorDataList.NumColors);
        Debug.Log(colorDataList.ColorList.Count);
    }

    // Changes the color options in the option buttons
    private void ChangeOptionButtons()
    {
        string previousButtonName = "";
        Color newButtonColor = Color.black;

        // Here we place the correct color at the start of the array and shuffle it. 
        colorDataList.GetRandomColorName(colorButton.CurrentColorName(), 0, true); 

        int offset = 1; // As the above line places the correct option at 0, an offset of 1 must be in place to not replace the correct color.
        int iter = 0;
        int correctButtonIteration = Random.Range(0, optionButtons.Count); 

        
        foreach (UIButton optionButton in optionButtons)
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

                optionButton.ChangeButtonVisual(previousButtonName, newButtonColor);
            }
            else
            {
                newButtonColor = colorDataList.GetRandomColor(0, false); 
                optionButton.ChangeButtonVisual(colorButton.CurrentColorName(), newButtonColor); // Apply correct color as it is the correct iteration.
            }
            
            iter += 1;
        }
    }


}
