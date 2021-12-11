using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ColorButton : MonoBehaviour
{
    private Text currentText;

    [Header("Colors")]
    // WARNING: Assigning colors and color texts must be in matching order. 
    [SerializeField] private Color[] colors; 
    [SerializeField] private String[] colorTexts;

    void Start()
    {
        currentText = GetComponent<Text>();
    }

    // Randomly changes color and text of button, called by UIButton when clicked on.
    public void ChangeText()
    {
        int newColorIndex = Random.Range(0, colorTexts.Length);
        currentText.text = colorTexts[newColorIndex];

        // Separate array is created, this array does not include index of the text to ensure different highlight color
        int[] validColors = new int[colorTexts.Length - 1];
        int iter = 0;
        for (int i = 0; i < colorTexts.Length; i++)
        {
            if (i != newColorIndex)
            {
                validColors[iter] = i;
                iter += 1;
            }
        }

        // Could not find a better way to apply custom colors from the inspector
        Color selectedColor = colors[validColors[Random.Range(0, validColors.Length)]];
        Color newColor = new Color(selectedColor.r, selectedColor.g, selectedColor.b);
        currentText.color = newColor;
    }
}
