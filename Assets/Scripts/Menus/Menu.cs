using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("Menu Attributes")]
    public MenuID ID;

    [Header("Color Data")]
    [SerializeField] protected RandomColors visualRandomColors;

    /// <summary>
    /// Resets the random colors data, such that they are shuffled but also ignores
    /// the background color.
    /// </summary>
    protected void ResetVisualRandomColors()
    {
        visualRandomColors.Reset(); // Reset the visual colors
        // Ignore the color of the background to persist into the new game
        visualRandomColors.IgnoreColor(GameManager.Instance.BackgroundColorData.ColorName);
    }
}
