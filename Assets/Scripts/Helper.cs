using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    /// <summary>
    /// Simple method for generating a custom color
    /// </summary>
    /// <param name="color">Custom RGB values to generate a color for</param>
    /// <returns>Color value that can be assigned to text</returns>
    public static Color GenColor(Color color)
    {
        return new Color(color.r, color.g, color.b);
    }

}

