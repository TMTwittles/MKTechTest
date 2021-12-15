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
}


