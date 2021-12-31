using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Random Colors", menuName = "Random Colors")]
public class RandomColors : ScriptableObject
{
    enum ColorArray
    {
        Primary,
        Secondary
    };


    [SerializeField] private List<ColorData> colorList;
    private Dictionary<string, ColorData> colorDictionary;
    private string[] colorArray;
    private int numColors = 0;
    private int lastIndex = 0; // Last index where a colour was placed, it will be reset to 0 if shuffle = false

    public List<ColorData> ColorList
    {
        get { return colorList; }
        set { colorList = value; }
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

        colorArray = new String[numColors];

        for (int i = 0; i < numColors; i++)
        {
            colorArray[i] = colorList[i].ColorName;
        }
    }

    private void GenerateDictionary()
    {
        colorDictionary = new Dictionary<string, ColorData>();

        foreach (ColorData colorData in ColorList)
        {
            colorDictionary.Add(colorData.ColorName, colorData);
        }
    }

    private void Swap(int i, int j)
    {
        string temp = colorArray[i];
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
    /// Modifies RGB color values of a specific color
    /// </summary>
    /// <param name="colorName"> String value, name of color to change </param>
    /// /// <param name="colorRGB"> Color value, color value to change color to </param>
    /// <returns> Color data of the specified color. </returns>
    public void SetColorData(string colorName, Color colorRGB)
    {
        if (colorDictionary == null)
            GenerateDictionary();

        colorDictionary[colorName].SetColorRGB(colorRGB);
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
        if (colorDictionary == null)
            GenerateDictionary();

        return colorDictionary[colorName];
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
            if (colorArray[i] == colorName)
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
        
        if (colorDictionary == null) 
            GenerateDictionary();
        

        string randomColorName = colorArray[Random.Range(lastIndex, numColors)];
        return colorDictionary[randomColorName];
    }
}
