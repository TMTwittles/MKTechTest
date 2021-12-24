using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredMenu : Menu
{
    [Header("ColorGroups")] 
    [SerializeField] protected List<Text> colorGroup01;
    [SerializeField] protected List<Text> colorGroup02;
    [SerializeField] protected List<Text> colorGroup03;
    [SerializeField] protected List<Text> colorGroup04;
    [SerializeField] protected List<Text> colorGroup05;
    protected List<List<Text>> colorGroups;

    /// <summary>
    /// Initializes the color groups, this should be called in Awake() or before
    /// calling UpdateColors().
    /// </summary>
    protected void InitializeColorGroups()
    {
        colorGroups = new List<List<Text>>();
        colorGroups.Add(colorGroup01);
        colorGroups.Add(colorGroup02);
        colorGroups.Add(colorGroup03);
        colorGroups.Add(colorGroup04);
        colorGroups.Add(colorGroup05);
    }

    /// <summary>
    /// Updates the colors randomly based on the conditions set in visual random colors
    /// and the text assigned in the respective color groups.
    /// </summary>
    protected void UpdateColors()
    {
        ColorData colorData = null;
        foreach (List<Text> texts in colorGroups)
        {
            colorData = visualRandomColors.GetRandomColor();
            visualRandomColors.IgnoreColor(colorData.ColorName);
            foreach (Text text in texts)
            {
                text.color = Helper.GenColor(colorData.ColorRGB);
            }
        }
    }
}

