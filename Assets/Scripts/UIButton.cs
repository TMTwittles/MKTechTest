using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [Header("Color button")]
    public ColorButton colorButton; // Assign publicly to each button, this is a bad solution needs to be changed later.

    // TODO: Needs to change, make a solution where each button is not responsible for changing the color.
    public void Press()
    {
        colorButton.ChangeText();
    }
}




