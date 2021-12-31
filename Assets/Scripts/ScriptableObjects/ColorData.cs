using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color", menuName = "Color Data")]
public class ColorData : ScriptableObject
{
    [SerializeField] private string colorName;
    public string ColorName
    {
        get { return colorName; }
    }

    [SerializeField] private Color colorRGB;

    public Color ColorRGB
    {
        get { return colorRGB; }
    }

    [SerializeField] private bool canBeModified;

    public bool CanBeModified
    {
        get { return canBeModified; }
    }

    /// <summary>
    /// Sets the RGB value of the Color Data, will return an error if the color data
    /// cannot be modified.
    /// </summary>
    /// <param name="colorName"> String value, name of color to change </param>
    /// /// <param name="colorRGB"> Color value, color value to change color to </param>
    /// <returns> Color data of the specified color. </returns>
    public void SetColorRGB(Color color)
    {
        if (CanBeModified)
        {
            colorRGB.r = color.r;
            colorRGB.g = color.g;
            colorRGB.b = color.b;
        }
        else
            Debug.LogError("ERROR: Selected color data can not be modified");
    }

    /// <summary>
    /// Inits colorRGB value by allocating memory to it.  
    /// </summary>
    public void InitializeColorRGB()
    {
        colorRGB = new Color(colorRGB.r, colorRGB.g, colorRGB.b);
    }
}


