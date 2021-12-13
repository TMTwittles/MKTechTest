using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Color data list", menuName = "Color Data List")]
public class ColorDataList : ScriptableObject
{
    [SerializeField] private List<ColorData> colorList;
    private ColorData[] colorArray;
    private int numColors = 0;


        public List<ColorData> ColorList
    {
        get { return colorList; }
    }

    public int NumColors
    {
        get { return numColors; }
    }

    private void GenColorArray()
    {
        numColors = colorList.Count;

        Debug.Log(numColors);

        colorArray = new ColorData[numColors];

        for (int i = 0; i < numColors; i++)
        {
            colorArray[i] = colorList[i];
        }

        foreach (ColorData cd in colorList)
        {
            Debug.Log(cd.ColorName);
        }
    }

    private void Swap(int i, int j)
    {
        ColorData temp = colorArray[i];
        colorArray[i] = colorArray[j];
        colorArray[j] = temp;
    }

    private void Shuffle()
    {
        for (int i = numColors - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, numColors);
            Swap(i, randomIndex);
        }
    }

    /// <summary>
    /// Returns the name of a random color. 
    /// </summary>
    /// <param name="shuffle">Boolean value, whether to shuffle array or not </param>
    /// <returns> string with a random color name. </returns>

    public string GetRandomColorName(bool shuffle)
    {
        if (numColors == 0)
        {
            GenColorArray();
        }

        if (shuffle)
        {
            Shuffle();
        }

        return colorArray[Random.Range(0, numColors)].ColorName;
    }

    /// <summary>
    /// Returns the name of a random color. 
    /// </summary>
    /// <param name="colorToIgnore">name of color that will be ignored </param>
    /// <param name="startIndex">This is where the ignored color is placed in the array, anything > start index will be randomly returned </param>
    /// <param name="shuffle">Boolean value, whether to shuffle array or not </param>
    /// <returns> string with a random color name. </returns>

    public string GetRandomColorName(String colorToIgnore, int startIndex, bool shuffle)
    {
        if (numColors == 0)
        {
            GenColorArray();
        }

        if (shuffle)
        {
            Shuffle();
        }

        for (int i = 0; i < numColors; i++)
        {
            if (colorArray[i].ColorName == colorToIgnore)
            {
                Swap(i, startIndex); // Place color to ignore at the specified start index
                break;
            }
        }

        return colorArray[Random.Range(startIndex + 1, numColors)].ColorName;
    }

    /// <summary>
    /// Returns the name of a random color. 
    /// </summary>
    /// <param name="startIndex">Anything > start index will be randomly returned </param>
    /// <param name="shuffle">Boolean value, whether to shuffle array or not </param>
    /// <returns> string with a random color name. </returns>

    public string GetRandomColorName(int startIndex, bool shuffle)
    {
        if (numColors == 0)
        {
            GenColorArray();
        }

        if (shuffle)
        {
            Shuffle();
        }

        return colorArray[Random.Range(startIndex + 1, numColors)].ColorName;
    }

    /// <summary>
    /// Returns the color RGB values of a random color. 
    /// </summary>
    /// <param name="colorToIgnore"> name of color that will be ignored </param>
    /// <param name="startIndex">This is where the ignored color is placed in the array, anything > start index will be randomly returned </param>
    /// <param name="shuffle">Boolean value, whether to shuffle array or not </param>
    /// <returns> string with a random color name. </returns>

    public Color GetRandomColor(String colorToIgnore, int startIndex, bool shuffle)
    {
        if (numColors == 0)
        {
            GenColorArray();
        }

        if (shuffle)
        {
            Shuffle();
        }

        for (int i = 0; i < numColors; i++)
        {
            if (colorArray[i].ColorName == colorToIgnore)
            {
                Swap(i, startIndex); // Place color to ignore at the specified start index
                break;
            }
        }

        return colorArray[Random.Range(startIndex + 1, numColors)].ColorRGB;
    }

    /// <summary>
    /// Returns the color RGB values of a random color. 
    /// </summary>
    /// <param name="startIndex"> Anything > start index will be randomly returned </param>
    /// <param name="shuffle">Boolean value, whether to shuffle array or not </param>
    /// <returns> string with a random color name. </returns>
    
    public Color GetRandomColor(int startIndex, bool shuffle)
    {
        if (numColors == 0)
        {
            GenColorArray();
        }

        if (shuffle)
        {
            Shuffle();
        }

        return colorArray[Random.Range(startIndex + 1, numColors)].ColorRGB;
    }

}
