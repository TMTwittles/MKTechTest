using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Color data list", menuName = "Color Data List")]
public class RandomColors : ScriptableObject
{
    enum ColorArray
    {
        Primary,
        Secondary
    };


    [SerializeField] private List<ColorData> colorList;
    private ColorData[] colorArray;
    private int numColors = 0;
    private int lastIndex = 0; // Last index where a colour was placed, it will be reset to 0 if shuffle = false

    public List<ColorData> ColorList
    {
        get { return colorList; }
    }

    public int NumColors
    {
        get { return numColors; }
    }

    public int LastIndex
    {
        get { return lastIndex; }
    }

    private void GenerateArray()
    {
        numColors = colorList.Count;

        colorArray = new ColorData[numColors];

        for (int i = 0; i < numColors; i++)
        {
            colorArray[i] = colorList[i];
        }
    }

    private void Swap(int i, int j)
    {
        ColorData temp = colorArray[i];
        colorArray[i] = colorArray[j];
        colorArray[j] = temp;
    }

    private void Shuffle(int endIndex)
    {
        for (int i = numColors - 1; i > endIndex; i--)
        {
            int randomIndex = Random.Range(0, numColors);
            Swap(i, randomIndex);
        }
    }

    /// <summary>
    /// Resets the randomness and ignored colors
    /// </summary>
    public void Reset()
    {
        if (numColors == 0)
            GenerateArray();

        Shuffle(0);
        lastIndex = 0;
    }

    /// <summary>
    /// Returns the color data of a specific color name.
    /// </summary>
    /// <param name="colorName"> String value, name of color to get </param>
    /// <returns> Color data of the specified color. </returns>
    public ColorData GetColorData(string colorName)
    {
        if (numColors == 0)
            GenerateArray();

        foreach (ColorData color in ColorList)
        {
            if (color.ColorName == colorName)
                return color;
        }

        return null;
    }

    /// <summary>
    /// Ignores a color value, this will place the color value in a section of the array where it will be ignored when asking for
    /// random colors. 
    /// </summary>
    /// <param name="colorName"> String value, name of color to ignore </param>
    /// <returns> RGB values of the specified color. </returns>
    public void IgnoreColor(string colorName)
    {
        if (numColors == 0)
            GenerateArray();

        for (int i = 0; i < numColors; i++)
        {
            if (colorArray[i].ColorName == colorName)
            {
                Swap(i, lastIndex);
                break;
            }
        }

        lastIndex += 1;
    }

    /// <summary>
    /// Returns a random color. 
    /// </summary>
    /// <returns> color data of a random color name. </returns>
    public ColorData GetRandomColor()
    {
        if (numColors == 0)
            GenerateArray();

        return colorArray[Random.Range(lastIndex, numColors)];
    }
}
