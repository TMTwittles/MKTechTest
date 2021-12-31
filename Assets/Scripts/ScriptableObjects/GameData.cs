using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    [SerializeField] private int totalAttempts;

    public int TotalAttempts
    {
        get
        {
            return totalAttempts;
        }
    }

    [SerializeField] private RandomColors defaultRandomColors;

    public RandomColors DefaultRandomColors
    {
        get
        {
            return defaultRandomColors;
        }
    }

    [SerializeField] private RandomColors customRandomColors;

    public RandomColors CustomRandomColors
    {
        get
        {
            return customRandomColors;
        }
    }

    [SerializeField] private bool enableDynamicBackground;

    public bool EnableDynamicBackground
    {
        get
        {
            return enableDynamicBackground;
        }
        set
        {
            enableDynamicBackground = value;
        }
    }

    public void InitializeCustomRandomColors()
    {
        foreach (ColorData colorData in customRandomColors.ColorList)
        {
            colorData.InitializeColorRGB();
        }
    }

}
