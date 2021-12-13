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
    [SerializeField] private Text currentText;

    [Header("Colors")]
    [SerializeField] private ColorDataList colorList;

    void Start()
    {
        
    }

    // Randomly changes color and text of button
    public void ChangeText()
    {
        currentText.text = colorList.GetRandomColorName(true);

        Color selectedColor = colorList.GetRandomColor(currentText.text, 0, true);
        Color newColor = new Color(selectedColor.r, selectedColor.g, selectedColor.b);
        currentText.color = newColor;
    }

    // Called by game manager to keep track of game statistics
    public string CurrentColorName()
    {
        return currentText.text;
    }
}
